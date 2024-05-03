namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public class DocumentNameToTemplateFileNameMapper : IDocumentNameToTemplateFileNameMapper
{
	public         string                      GetTemplateFileName(string formName) => DocumentFileNameMap[formName];
	private static IDictionary<string, string> DocumentFileNameMap                  => s_documentFileNameMap.Value;

	// todo: update to read from either config or DB
	private static readonly Lazy<IDictionary<string, string>> s_documentFileNameMap = new(() => new Dictionary<string, string>(new DocumentNameComparer())
																								{
																									{ "acord.25", "acord.25.pdf" },
																									{ "acord.35", "acord.35.pdf" },
																									{ "acord.36", "acord.36.pdf" },
																									{ "acord.37", "acord.37.pdf" },
																									{ "acord.50", "acord.50.pdf" },
																									{ "acord.125", "acord.125.pdf" },
																									{ "acord.126", "acord.126.pdf" },
																									{ "acord.127", "acord.127.pdf" },
																									{ "acord.129", "acord.129.pdf" },
																									{ "acord.131", "acord.131.pdf" },
																									{ "acord.137", "acord.137.pdf" },
																									{ "acord.163", "acord.163.pdf" },
																									{ "Supplemental Operator Schedule", "Supplemental Operator Schedule.pdf" },
																									{ "90 percent application", "90 percent application.pdf" }
																								});
}