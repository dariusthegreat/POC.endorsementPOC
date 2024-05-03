using System.Text;
using ConsoleDump;

namespace TechQ.DocumentManagement.PdfPopulation;

// ReSharper disable once UnusedType.Global
public class IronPdfPopulator : IPdfPopulator
{
	private static readonly object s_syncLock = new();
	private static          bool   s_licenseApplied;

    // ReSharper disable once UnusedMember.Global
    public static void DisplayText(string filePath) => new StringBuilder()
                                                       .AppendLine("\n\n")
                                                       .AppendLine($"file path: {filePath}")
                                                       .AppendLine("text:")
                                                       .AppendLine(GetDocumentText(filePath))
                                                       .Dump();

    // ReSharper disable once MemberCanBePrivate.Global
    public static string GetDocumentText(string filePath)
    {
        ApplyLicense();
        using var doc = PdfDocument.FromFile(filePath);
        return doc.ExtractAllText();
    }

    public void Populate(IDictionary<string, string> fieldValues, string sourceFilePath, string destinationFilePath)
    {
		ApplyLicense();

		using var doc = PdfDocument.FromFile(sourceFilePath);

		var fieldValuePairs = from field in doc.Form
							  join pair in fieldValues
							  on field.FullName equals pair.Key
							  select new { field, pair.Value };

		foreach (var x in fieldValuePairs)
		{
			try
			{
				x.field.Value = x.Value;
			}
			catch (Exception e)
			{
				Console.Out.WriteLine($"invalid field name: {x.field.FullName}");
				Console.Out.WriteLine($"{e.GetType()} caught: {e.Message}");
				throw;
			}
		}

		doc.SaveAs(destinationFilePath);
	}

	private static void ApplyLicense()
	{
		lock (s_syncLock)
		{
			if (s_licenseApplied)
				return;

			License.LicenseKey = File.ReadAllText(@"IronPdf.license.txt");
			s_licenseApplied   = true;
		}
	}
}
