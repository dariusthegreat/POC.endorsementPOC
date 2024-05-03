using System.Diagnostics.Contracts;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public class DocumentGenerationRequest
{
	private static int s_nextId;
	
	public string RequestId        { get; } = GetNextId();
	public string OutputFolderPath { get; init; }
	public string DocumentName { get;  }

	public DocumentGenerationRequest() {	}

	public DocumentGenerationRequest(string documentName, string outputFolderPath)
	{
		DocumentName = documentName ?? throw new ArgumentNullException(nameof(documentName));
		OutputFolderPath = outputFolderPath ?? throw new ArgumentNullException(nameof(outputFolderPath));
	}

	private static string GetNextId() => FormatId(Interlocked.Increment(ref s_nextId));

	[Pure]
	private static string FormatId(int id)
	{
		var str = id.ToString().PadLeft(8, '0');
		return $"{str[0..4]}-{str[4..]}";
	}
}