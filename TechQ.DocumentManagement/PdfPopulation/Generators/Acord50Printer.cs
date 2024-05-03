using TechQ.Core.Extensions;
using TechQ.Entities;
using TechQ.Entities.Models;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

[DocumentName("acord 50")]
public class Acord50Printer : DocumentPrinterBase
{
	public Acord50Printer(string documentTemplatesFolderPath, IInsuranceDbContext dbContext, long clientId, long agencyId, long agentId, IDocumentNameToTemplateFileNameMapper documentTemplateMapper = null) : base(documentTemplatesFolderPath, dbContext, clientId, agencyId, agentId, documentTemplateMapper) { }

	protected override IEnumerable<DocumentGenerationElements> GetDocumentElements(DocumentGenerationRequest request)
	{
		var allFields        = new KeyValuePairGenerator(DbContext, ClientId, AgencyId, AgentId).GetPairs();
		var nonVehicleFields = allFields.AntiJoin("Veh,VehYear,VehMake,VehModelVehBodyType,VehGvw,VIN,VehicleYear,VehicleMakeModel".Split(','), pair => s_removeEndIndexRegex.Replace(pair.Key, ""), name => name).ToArray();

		var templateFilePath = Path.Combine(DocumentTemplatesFolderPath, _documentTemplateMapper.GetTemplateFileName("acord 50"));

		var vehicles = DbContext.Vehicles
								.Where(x => x.AgencyClientId == ClientId)
								.ToArray();

		return vehicles
			   .Select((vehicle, index) => new
										   {
											   fields = nonVehicleFields.Concat(GetVehicleFields(vehicle)), 
											   outputFilePath = Path.Combine(request.OutputFolderPath, $"{request.RequestId}.acord-50-{(index + 1).ToString().PadLeft(3, '0')}.pdf"), 
											   description = $"Acord 50 #{index + 1} of {vehicles.Length}"
										   })
			   .Select(x => new DocumentGenerationElements(new(x.fields),
														   templateFilePath,
														   x.outputFilePath,
														   "acord-50",
														   x.description));
	}

	private static Dictionary<string, string> GetVehicleFields(Vehicle vehicle) => new()
																				   {
																					   { "VIN", vehicle.VehicleVinNumber }, 
																					   { "VehicleYear", vehicle.VehicleYear.ToString() }, 
																					   { "VehicleMakeModel", $"{vehicle.VehicleMake} {vehicle.VehicleModel}" }
																				   };
}