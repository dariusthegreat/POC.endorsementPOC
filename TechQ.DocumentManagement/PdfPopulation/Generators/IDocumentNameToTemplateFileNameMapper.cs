namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public interface IDocumentNameToTemplateFileNameMapper
{
	string GetTemplateFileName(string formName);
}