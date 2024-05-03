using System.Reflection;
using TechQ.Entities;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

/// <summary>
/// Document generator factory. i.e., a factory that creates something that can be used to produce a specific document 
/// </summary>
/// <remarks>
/// The logic is broken down into 2 parts:
/// 1. DocumentGeneratorFactory
/// 2. GeneratorFactory
///
/// 1. DocumentGeneratorFactory: Given a specific document type, finds the correct Document Generator Type. 
/// 2. GeneratorFactory: once the document generator is resolved, a GeneratorFactory is used 
/// 
/// </remarks>
// ReSharper disable once UnusedType.Global
public class DocumentGeneratorFactory : IDocumentGeneratorFactory
{
	private readonly IEqualityComparer<string> _documentNameComparer;

	// ReSharper disable once MemberCanBePrivate.Global
	public string TemplatesFolderPath { get; }

	// ReSharper disable once ConvertToPrimaryConstructor
	public DocumentGeneratorFactory(string templatesFolderPath, IEqualityComparer<string> documentNameComparer = null)
	{
		TemplatesFolderPath = templatesFolderPath ?? throw new ArgumentNullException(nameof(templatesFolderPath));
		_documentNameComparer = documentNameComparer ?? new DocumentNameComparer();
	}

	public IDocumentPrinter Create(string documentName, IInsuranceDbContext dbContext, long clientId, long agencyId, long agentId)
	{
		var printerType = typeof(DocumentGeneratorFactory)
			.Assembly
			.GetTypes()
			.Where(x => x.Namespace == typeof(DocumentGeneratorFactory).Namespace)
			.Where(x => x.IsClass)
			.Where(x => !x.IsAbstract)
			.Where(x => typeof(DocumentPrinterBase).IsAssignableFrom(x))
			.SelectMany(type => type.GetCustomAttributes<DocumentNameAttribute>().Select(attribute => new { type, documentName = attribute.Name }))
			.Single(x => _documentNameComparer.Equals(x.documentName, documentName))
			.type;

		var args = new object[] { TemplatesFolderPath, dbContext, clientId, agencyId, agentId, new DocumentNameToTemplateFileNameMapper() };

		return (IDocumentPrinter)Activator.CreateInstance(printerType!, args);
	}
}