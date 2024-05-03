using TechQ.Entities;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

[DocumentName("Acord 25")]
[DocumentName("Acord 35")]
[DocumentName("Acord 36")]
[DocumentName("Acord 36")]
[DocumentName("Acord 125")]
[DocumentName("Acord 126")]
[DocumentName("Acord 129")]
[DocumentName("Acord 131")]
[DocumentName("Acord 131")]
[DocumentName("Acord 131")]
[DocumentName("Acord 163")]
[DocumentName("Supplemental Operator Schedule")]
[DocumentName("90 percent application")]
// ReSharper disable once UnusedType.Global
public class DefaultPrinter : DocumentPrinterBase
{
	// ReSharper disable once ConvertToPrimaryConstructor
	public DefaultPrinter(string documentTemplatesFolderPath, IInsuranceDbContext dbContext, long clientId, long agencyId, long agentId, IDocumentNameToTemplateFileNameMapper documentTemplateMapper = null) : base(documentTemplatesFolderPath, dbContext, clientId, agencyId, agentId, documentTemplateMapper) { }

	protected override IEnumerable<DocumentGenerationElements> GetDocumentElements(DocumentGenerationRequest request)
	{
		var allFields        = new KeyValuePairGenerator(DbContext, ClientId, AgencyId, AgentId).GetPairs();
		var templateFileName = _documentTemplateMapper.GetTemplateFileName(request.DocumentName);
		var templateFilePath = Path.Combine(DocumentTemplatesFolderPath, templateFileName);
		var destination      = Path.Combine(request.OutputFolderPath,    $"{request.RequestId}.{templateFileName}");

		yield return new(new(allFields), templateFilePath, destination, request.DocumentName, request.DocumentName);
	}
}