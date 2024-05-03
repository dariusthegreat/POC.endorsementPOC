using System.Diagnostics.Contracts;
using System.Text;
using Newtonsoft.Json;

public static class SessionHelpers
{
	public static string GetSessionId() => $"{DateTimeOffset.Now:yyyyMMddTHHmmsszzz}".Replace(":", "");

	public static IDictionary<string, string> DumpKeyValuePairsToFile(this IDictionary<string, string> fieldValues, string sessionId)
	{
		var jsonFileName = $"{sessionId}.json";
		var jsonFilePath = Path.Combine(Environment.CurrentDirectory, jsonFileName);
		File.WriteAllLines(jsonFilePath, [JsonConvert.SerializeObject(fieldValues, Formatting.Indented)]);

		var textFileName = $"{sessionId}.text";
		var textFilePath = Path.Combine(Environment.CurrentDirectory, textFileName);
		using StreamWriter streamWriter = new(textFilePath);
		return fieldValues.DumpKeyValuePairs(streamWriter);
	}

	public static IDictionary<string, string> DumpKeyValuePairsToTextFile(this IDictionary<string, string> fieldValues, string sessionId, string folderPath = null)
	{
		folderPath ??= Environment.CurrentDirectory;

		if (!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);

		var textFileName = $"{sessionId}.text";
		var textFilePath = Path.Combine(folderPath, textFileName);
		using StreamWriter streamWriter = new(textFilePath);
		return fieldValues.DumpKeyValuePairs(streamWriter);
	}

	public static IDictionary<string, string> DumpKeyValuePairsToJsonFile(this IDictionary<string, string> fieldValues, string sessionId, string folderPath=null)
	{
		folderPath ??= Environment.CurrentDirectory;
		
		if(!Directory.Exists(folderPath))
			Directory.CreateDirectory(folderPath);
		
		var jsonFileName = $"{sessionId}.json";
		var jsonFilePath = Path.Combine(folderPath, jsonFileName);
		File.WriteAllLines(jsonFilePath, [JsonConvert.SerializeObject(fieldValues, Formatting.Indented)]);
		return fieldValues;
	}

	public static IDictionary<string, string> DumpKeyValuePairs(this IDictionary<string, string> fieldValues, TextWriter output=null)
	{
		output ??= Console.Out;

		var text = new StringBuilder()
			.AppendLine("\n\n")
			.AppendLine(new string('#', 120))
			.AppendLine("Generated key/value pairs:")
			.AppendLine(JsonConvert.SerializeObject(fieldValues, Formatting.Indented))
			.AppendLine(new string('#', 120))
			.AppendLine("\n")
			.ToString();

		output.WriteLine(text);

		return fieldValues;
	}

	[Pure]
	public static string ToAbsolutePath(this string path) => new FileInfo(path).FullName;
}