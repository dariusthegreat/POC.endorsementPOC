namespace TechQ.DocumentManagement.PdfPopulation;

public static class IronPdfExtensions
{
	public static string[] GetFieldNames(this IronPdf.PdfDocument doc) => doc.Form.Select(x => x.FullName).ToArray();
}
