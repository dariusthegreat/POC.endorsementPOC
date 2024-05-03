using System.Text.RegularExpressions;
using TechQ.Entities;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public abstract class DocumentPrinterBase : IDocumentPrinter
{
	protected readonly IDocumentNameToTemplateFileNameMapper _documentTemplateMapper;
	private readonly   IPdfPopulator                         _pdfPopulator;

	protected string              DocumentTemplatesFolderPath { get; }
	protected IInsuranceDbContext DbContext                   { get; }
	protected long                ClientId                    { get; }
	protected long                AgencyId                    { get; }
	protected long                AgentId                     { get; }


	// ReSharper disable once ConvertToPrimaryConstructor
	protected DocumentPrinterBase(string                                documentTemplatesFolderPath,
	                              IInsuranceDbContext                   dbContext,
	                              long                                  clientId,
	                              long                                  agencyId,
	                              long                                  agentId,
	                              IDocumentNameToTemplateFileNameMapper documentTemplateMapper = null,
	                              IPdfPopulator                         pdfPopulator           = null)
	{
		DocumentTemplatesFolderPath = documentTemplatesFolderPath ?? throw new ArgumentNullException(nameof(documentTemplatesFolderPath));
		DbContext                   = dbContext                   ?? throw new ArgumentNullException(nameof(dbContext));
		ClientId                    = clientId;
		AgencyId                    = agencyId;
		AgentId                     = agentId;
		_documentTemplateMapper     = documentTemplateMapper ?? new DocumentNameToTemplateFileNameMapper();
		_pdfPopulator               = pdfPopulator           ?? new AsposePdfPopulator();
	}

	public DocumentGenerationResponse PrintDocument(DocumentGenerationRequest request)
	{
		var printed = GetDocumentElements(request)
					  .Select(x =>
							  {
								  _pdfPopulator.Populate(x.Pairs, x.TemplateFilePath, x.DestinationFilePath);
								  return new GeneratedDocument(x.FormName, x.Description, x.DestinationFilePath);
							  })
					  .ToArray();

		return new(printed);
	}

	protected abstract IEnumerable<DocumentGenerationElements> GetDocumentElements(DocumentGenerationRequest request);

	protected record DocumentGenerationElements(Dictionary<string, string> Pairs, string TemplateFilePath, string DestinationFilePath, string FormName, string Description);

	protected static readonly Regex s_removeEndIndexRegex = new("([0-9]+)$", RegexOptions.Compiled);
}