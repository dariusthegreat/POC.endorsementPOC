using TechQ.Entities;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public interface IDocumentGeneratorFactory
{
	IDocumentPrinter Create(string documentName, IInsuranceDbContext dbContext, long clientId, long agencyId, long agentId);
}