using System.Collections;
using Newtonsoft.Json;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public class DocumentGenerationResponse(params GeneratedDocument[] generatedDocuments) :	IEnumerable<string>, 
																							ILookup<string, string>, 
																							IReadOnlyDictionary<string, GeneratedDocument>
{
	private readonly GeneratedDocument[] _generatedDocuments = generatedDocuments ?? throw new ArgumentNullException(nameof(generatedDocuments));

	#region IEnumerable<string> implementation
		
	private IEnumerable<string> FilePaths                => _generatedDocuments.Select(x => x.FilePath);
	private IEnumerator<string> GetFilePathsEnumerator() => FilePaths.GetEnumerator();

	IEnumerator IEnumerable.                GetEnumerator() => GetFilePathsEnumerator();
	IEnumerator<string> IEnumerable<string>.GetEnumerator() => GetFilePathsEnumerator();
	
	#endregion
	
	#region ILookup<string,string> implementation

	private ILookup<string, string> FormNameToFilePathLookup => _generatedDocuments.ToLookup(x => x.FormName, x => x.FilePath);

	IEnumerator<IGrouping<string, string>> IEnumerable<IGrouping<string, string>>.GetEnumerator()      => FormNameToFilePathLookup.GetEnumerator();
	bool ILookup<string, string>.                                                 Contains(string key) => FormNameToFilePathLookup.Contains(key);
	int ILookup<string, string>.                                                  Count                => FormNameToFilePathLookup.Count;

	IEnumerable<string> ILookup<string, string>.this[string key] => FormNameToFilePathLookup[key];

	#endregion
	
	#region IDictionary<string, GeneratedDocument> implementation

	private IReadOnlyDictionary<string, GeneratedDocument> FilePathToDocumentMap => _generatedDocuments.ToDictionary(x => x.FilePath, x => x, new DocumentNameComparer());

	IEnumerator<KeyValuePair<string, GeneratedDocument>> IEnumerable<KeyValuePair<string, GeneratedDocument>>.GetEnumerator()                                      => FilePathToDocumentMap.GetEnumerator();
	int IReadOnlyCollection<KeyValuePair<string, GeneratedDocument>>.                                         Count                                                => FilePathToDocumentMap.Count;
	bool IReadOnlyDictionary<string, GeneratedDocument>.                                                      ContainsKey(string key)                              => FilePathToDocumentMap.ContainsKey(key);
	bool IReadOnlyDictionary<string, GeneratedDocument>.                                                      TryGetValue(string key, out GeneratedDocument value) => FilePathToDocumentMap.TryGetValue(key, out value);

	GeneratedDocument IReadOnlyDictionary<string, GeneratedDocument>.this[string key] => FilePathToDocumentMap[key];

	IEnumerable<string> IReadOnlyDictionary<string, GeneratedDocument>.           Keys   => FilePathToDocumentMap.Keys;
	IEnumerable<GeneratedDocument> IReadOnlyDictionary<string, GeneratedDocument>.Values => FilePathToDocumentMap.Values;

	#endregion

	public override string ToString() => JsonConvert.SerializeObject(_generatedDocuments, Formatting.Indented);
}