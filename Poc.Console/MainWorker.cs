using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ConsoleDump;
using TechQ.DocumentManagement.PdfPopulation;

namespace Poc.Console;

public class MainWorker(IPdfPopulator pdfPopulator, IDictionary<string, string> fieldValues)
{
	private IPdfPopulator PdfPopulator { get; } = pdfPopulator ?? throw new ArgumentNullException(nameof(pdfPopulator));
	private IDictionary<string, string> FieldValues { get; } = fieldValues ?? throw new ArgumentNullException(nameof(fieldValues));
	public string SessionId { get; init; }


	// ReSharper disable once UnusedMethodReturnValue.Global
	public bool PopulateAllPdfDocuments(string sourceFolderPath, string destinationFolderPath, params string[] formNumbers)
	{
		if (destinationFolderPath == null) throw new ArgumentNullException(nameof(destinationFolderPath));

		ThrowIfNullOrEmptyOrWhiteSpace(sourceFolderPath);
		ThrowIfNullOrEmptyOrWhiteSpace(destinationFolderPath);

		if (!Directory.Exists(destinationFolderPath)) 
			Directory.CreateDirectory(destinationFolderPath);

		var tuples =
		(
			from sourceFilePath in Directory.EnumerateFiles(sourceFolderPath, "*.pdf", SearchOption.TopDirectoryOnly).Dump("available PDF templates")
									.Where(x => !formNumbers.Any() || formNumbers.Contains(GetAcordFormNumber(x))).Dump("PDF templates to populate")
									.Select(x => x.ToAbsolutePath())

			let destinationFilePath = PopulateSingleDocument(sourceFilePath, destinationFolderPath).ToAbsolutePath()
			let process = new Process { StartInfo = new ProcessStartInfo(destinationFilePath) { UseShellExecute = true } }

			select new { sourceFilePath, destinationFilePath, process }
		).ToArray();

		tuples.Select(x => new { x.sourceFilePath, x.destinationFilePath }).Dump("populated PDFs");


		return tuples.All(x => x.process.Start()).Dump("all populated PDF files found & launched?");
	}

	
	private string PopulateSingleDocument(string sourceFilePath, string destinationFolderPath)
	{
		var destinationFilePath = GetDestinationFilePath(sourceFilePath, destinationFolderPath);
		PdfPopulator.Populate(FieldValues, sourceFilePath, destinationFilePath);
		return destinationFilePath;
	}

	private string GetDestinationFilePath(string sourcePath, string destinationFolderPath)
	{
		ThrowIfNullOrEmptyOrWhiteSpace(sourcePath);
		ThrowIfNullOrEmptyOrWhiteSpace(destinationFolderPath);

		if (SessionId!=null)
		{
			var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourcePath);
			var extension = Path.GetExtension(sourcePath);
			var fileName = $"{fileNameWithoutExtension}.populated.{SessionId}{extension}";
			return Path.Combine(destinationFolderPath, fileName);
		}

		return EnumeratePaths(sourcePath, destinationFolderPath).First(x => !File.Exists(x));

		static IEnumerable<string> EnumeratePaths(string sourceFilePath, string destinationFolderPath)
		{
			ThrowIfNullOrEmptyOrWhiteSpace(sourceFilePath);
			ThrowIfNullOrEmptyOrWhiteSpace(destinationFolderPath);

			var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sourceFilePath);
			var extension = Path.GetExtension(sourceFilePath).TrimStart('.');

			for (int i = 0; i < int.MaxValue; i++)
			{
				var index = i.ToString().PadLeft(8, '0');
				var fileName = $"{fileNameWithoutExtension}.populated.{index[..4]}-{index[^4..]}.{extension}";
				yield return Path.Combine(destinationFolderPath, fileName);
			}
		}
	}

	private static void ThrowIfNullOrEmptyOrWhiteSpace(string parameter, [CallerArgumentExpression(nameof(parameter))] string parameterName = default)
	{
		if (parameter == null) throw new ArgumentNullException(parameterName);
		if (parameter == "") throw new ArgumentException($"Invalid (empty) '{parameterName}'.", parameterName);
		if (string.IsNullOrWhiteSpace(parameter)) throw new ArgumentException($"Invalid '{parameterName}'. Value cannot solely consist of whitespaces.", parameterName);
	}

	private static string GetAcordFormNumber(string filePath)
	{
		var title = Path.GetFileNameWithoutExtension (filePath);
		var match = AcordFormNumberRegex.Match(title);
		return match.Success ? match.Groups["formNumber"].Value: null;
	}

	private static readonly Lazy<Regex> s_lazyLoadedAcordNumberRegex = new(() => new Regex(@"\.(?<formNumber>[0-9]{2,3})$", RegexOptions.Compiled));
	private static Regex AcordFormNumberRegex => s_lazyLoadedAcordNumberRegex.Value;

}

