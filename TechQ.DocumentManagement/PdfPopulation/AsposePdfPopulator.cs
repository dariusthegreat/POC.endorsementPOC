using Aspose.Pdf.Forms;

namespace TechQ.DocumentManagement.PdfPopulation;

public class AsposePdfPopulator : IPdfPopulator
{
	private static readonly object s_syncLock = new();
    private static          bool   s_licenseApplied;

	public void Populate(IDictionary<string, string> fieldValues, string sourceFilePath, string destinationFilePath)
	{
		ApplyLicense();

		using Aspose.Pdf.Document doc = new(sourceFilePath);

		var pairs = doc.Form.Fields
			.Join(fieldValues, x => x.FullName, x => x.Key, (field, pair) => new { field, pair.Value })
			.ToArray();

		var names = "90% App - Radius - less than 100,90% App - Radius - 100 - 200,90% App - Radius - 200 - 500,90% App - Radius - 500+,90% App - Radius - 12 Western States,90% App - Radius - unlimited".Split(',');

		var matchedFields = doc.Form.Fields.Join(names, x => x.FullName, name => name, (field, _) => field).ToArray();
		var matchedPairs = fieldValues.Join(names, x => x.Key, x => x, (pair, _) => pair).ToArray();

		if (matchedFields.Length != names.Length)
			throw new ApplicationException($"expected {names.Length} field matches. found: {matchedFields.Length}");

		if (matchedPairs.Length != names.Length)
			throw new ApplicationException($"expected {names.Length} pair matches. found: {matchedPairs.Length}");

		foreach (var x in pairs)
		{
			try
			{
				switch(x.field)
				{
					case CheckboxField checkboxField:
					{
						checkboxField.Checked = !string.IsNullOrEmpty(x.Value);
						break;
					}
					case RadioButtonField radioButton:
					{
						radioButton.Selected = string.IsNullOrEmpty(x.Value) ? -1 : 1;
						break;
					}
					default:
					{
						x.field.Value = x.Value;
						break;
					}
				}
			}
			catch (Exception e)
			{
				Console.Error.WriteLine($"invalid field name: {x.field.FullName}");
				Console.Error.WriteLine($"{e.GetType()} caught: {e.Message}");
				throw;
			}
		}

		doc.Save(destinationFilePath);
	}

	private static void ApplyLicense()
    {
	    lock (s_syncLock)
	    {
		    if (s_licenseApplied)
			    return;

		    new Aspose.Pdf.License().SetLicense("Aspose.PDFProductFamily.license");
		    s_licenseApplied = true;
		}
    }
}
