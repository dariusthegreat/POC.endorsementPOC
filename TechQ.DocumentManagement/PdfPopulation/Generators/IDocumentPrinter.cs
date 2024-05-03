namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public interface IDocumentPrinter
{
	DocumentGenerationResponse PrintDocument(DocumentGenerationRequest request);
}