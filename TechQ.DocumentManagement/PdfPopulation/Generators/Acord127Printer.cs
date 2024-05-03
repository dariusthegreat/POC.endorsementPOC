using TechQ.Core.Extensions;
using TechQ.Entities;
using TechQ.Entities.Models;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

[DocumentName("acord 127")]
public class Acord127Printer : DocumentPrinterBase
{
	public Acord127Printer(string documentTemplatesFolderPath, IInsuranceDbContext dbContext, long clientId, long agencyId, long agentId, IDocumentNameToTemplateFileNameMapper documentTemplateMapper = null) : base(documentTemplatesFolderPath, dbContext, clientId, agencyId, agentId, documentTemplateMapper) { }

	private record ElementAndIndex<T>(T Element, int Index);

	protected override IEnumerable<DocumentGenerationElements> GetDocumentElements(DocumentGenerationRequest request)
	{
		var drivers        = DbContext.Drivers.Where(x => x.AgencyClientId == ClientId).ToArray();
		var allPairs       = new KeyValuePairGenerator(DbContext, ClientId, AgencyId, AgentId).GetPairs();
		var nonDriverPairs = allPairs.AntiJoin("Driver,DriverName,DriverFirstName,DriverLastName,DriverDLNumber,DriverDOB,DriverStateLicense".Split(','), x => s_removeEndIndexRegex.Replace(x.Key, ""), x => x);

		var acord127TemplateFilePath = Path.Combine(DocumentTemplatesFolderPath, _documentTemplateMapper.GetTemplateFileName("acord 127"));
		var acord163TemplateFilePath = Path.Combine(DocumentTemplatesFolderPath, _documentTemplateMapper.GetTemplateFileName("acord 163"));

		var acord127OutputFilePath = Path.Combine(request.OutputFolderPath, $"{request.RequestId}.acord-127.pdf");
		string GetAcord163OutputFilePath(int groupId) => Path.Combine(request.OutputFolderPath, $"{request.RequestId}.acord-127-163-{groupId.ToString().PadLeft(3, '0')}.pdf");

		var count163 = drivers.Length < 13 ? 0 : 1 + ((drivers.Length - 13) / 24);
		var field163 = new Dictionary<string,string> { {  "163", count163 > 0 ? "X" : "" } };

		return drivers.Select((driver, index) => new ElementAndIndex<Driver>(driver, index))
					  .GroupBy(x => x.Index < 13 ? 0 : 1 + ((x.Index - 13) / 24))
					  .Select(GetGroupElements);

		DocumentGenerationElements GetGroupElements(IGrouping<int, ElementAndIndex<Driver>> group)
		{
			var driverFields = group.Select((x, i) => GetDriverFields(x.Element, i, i + 1 + Math.Min(1, group.Key) * (13 + ((group.Key-1) * 24)))).SelectMany(x => x);
			var fields       = new Dictionary<string, string>(nonDriverPairs.Concat(field163).Concat(driverFields));

			var formName         = group.Key == 0 ? "acord-127" : $"acord-163";
			var description      = group.Key == 0 ? "Acord 127" : $"Acord 163 #{group.Key} of {count163}";
			var templateFilePath = group.Key == 0 ? acord127TemplateFilePath : acord163TemplateFilePath;
			var outputFilePath   = group.Key == 0 ? acord127OutputFilePath : GetAcord163OutputFilePath(group.Key);

			return new(fields, templateFilePath, outputFilePath, formName, description);
		}
	}

	static IEnumerable<KeyValuePair<string, string>> GetDriverFields(Driver item, int index, int driverNumber)
	{
		yield return new($"Driver{index             + 1}", $"{driverNumber}");
		yield return new($"DriverName{index         + 1}", $"{item.DriverFirstName} {item.DriverMiddleName} {item.DriverLastName}");
		yield return new($"DriverFirstName{index    + 1}", item.DriverLastName);
		yield return new($"DriverLastName{index     + 1}", item.DriverLastName);
		yield return new($"DriverDLNumber{index     + 1}", item.DriverLicenseNumber);
		yield return new($"DriverDOB{index          + 1}", $"{item.DriverDob}");
		yield return new($"DriverStateLicense{index + 1}", item.DriverLicenseState);
	}
}