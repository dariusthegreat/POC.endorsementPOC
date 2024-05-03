namespace TechQ.DocumentManagement.PdfPopulation;

public interface IPdfPopulator
{
	void Populate(IDictionary<string, string> fieldValues, string sourceFilePath, string destinationFilePath);
}
