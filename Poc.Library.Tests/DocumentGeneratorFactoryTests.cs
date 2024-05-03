using System.Text;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TechQ.Core;
using TechQ.DocumentManagement.PdfPopulation.Generators;
using TechQ.Entities.Models;

namespace TechQ.DocumentManagement.Tests;

//[TestClass]
public class DocumentGeneratorFactoryTests : IDisposable
{
	private readonly DocumentGeneratorFactory _sut       = new(TemplatesFolderPath);
	private readonly InsuranceDbContext       _dbContext = new(DatabaseConnections["InsuranceDb"]);

	private static readonly Lazy<IDictionary<string, DatabaseConnectionDetails>> s_lazyLoadedDatabaseConnectionDetails = new(GetDatabaseConnections);
	private static readonly Lazy<IConfigurationRoot> s_lazyLoadedConfiguration = new(LoadConfiguration);

	private static IConfigurationRoot Configuration => s_lazyLoadedConfiguration.Value;
	private static IDictionary<string, DatabaseConnectionDetails> DatabaseConnections => s_lazyLoadedDatabaseConnectionDetails.Value;

	private static string PdfOutputFolderPath => Configuration.GetValue<string>(@"pdfOutputFolderPath");
	private static string TemplatesFolderPath => Configuration.GetValue<string>(@"pdfTemplatesFolderPath");


	public TestContext TestContext { get; set; }


	private static IConfigurationRoot LoadConfiguration() => new ConfigurationBuilder()
															 .SetBasePath(Directory.GetCurrentDirectory())
															 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
															 .Build();


	private static Dictionary<string, DatabaseConnectionDetails> GetDatabaseConnections() => Configuration.GetSection("dbConnections")
																											.Get<Dictionary<string, DatabaseConnectionDetails>>()
																											.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);


	private void WriteLine(string message) => TestContext.WriteLine(message);

	[TestMethod]
	public void DbConnectionDetailsShouldBeRetrieved()
	{
		Assert.IsNotNull(Configuration.GetSection("dbConnections"));
		Assert.IsNotNull(Configuration.GetSection("dbConnections").Get<Dictionary<string, DatabaseConnectionDetails>>());
		Assert.IsNotNull(Configuration.GetSection("dbConnections").Get<Dictionary<string, DatabaseConnectionDetails>>().ToDictionary(x=>x.Key, x=>x.Value, StringComparer.OrdinalIgnoreCase));

		var insuranceDbDetails = DatabaseConnections["InsuranceDb"];
		WriteLine($"connection string: {insuranceDbDetails}");
	}

	
	[TestMethod]
	public void PdfOutputFolderPathConfigValueShouldBeAccessible()
	{
		WriteLine($"PDF output folder path: {PdfOutputFolderPath}");
		Assert.IsNotNull(PdfOutputFolderPath);
		Assert.IsFalse(string.IsNullOrWhiteSpace(PdfOutputFolderPath));
	}

		
	[TestMethod]
	public void FormsShouldPrintAcord25()
	{
		var generator = _sut.Create("ACORD 25", _dbContext, 1, 2, 1);
		Assert.IsNotNull(generator);
		Assert.IsInstanceOfType<DefaultPrinter>(generator);

		if (!Directory.Exists(PdfOutputFolderPath))
			Directory.CreateDirectory(PdfOutputFolderPath);

		var request = new DocumentGenerationRequest("Acord 25", PdfOutputFolderPath);

		var response = generator.PrintDocument(request);
		Assert.IsNotNull(response);
		WriteLine(JsonConvert.SerializeObject(response));
	}

	[TestMethod]
	public void FormsShouldPrintAcord50()
	{
		var generator = _sut.Create("ACORD 50", _dbContext, 1, 2, 1);
		Assert.IsNotNull(generator);
		Assert.IsInstanceOfType<Acord50Printer>(generator);

		if (!Directory.Exists(PdfOutputFolderPath))
			Directory.CreateDirectory(PdfOutputFolderPath);

		var request = new DocumentGenerationRequest("Acord 50", PdfOutputFolderPath);

		var response = generator.PrintDocument(request);
		Assert.IsNotNull(response);
		WriteLine(JsonConvert.SerializeObject(response));
	}

	[TestMethod]
	public void FormsShouldPrintAcord127()
	{
		var generator = _sut.Create("ACORD 127", _dbContext, 1, 2, 1);
		Assert.IsNotNull(generator);
		Assert.IsInstanceOfType<Acord127Printer>(generator);

		if (!Directory.Exists(PdfOutputFolderPath))
			Directory.CreateDirectory(PdfOutputFolderPath);

		var request = new DocumentGenerationRequest("Acord 127", PdfOutputFolderPath);

		var response = generator.PrintDocument(request);
		Assert.IsNotNull(response);
		WriteLine(JsonConvert.SerializeObject(response));
	}

	[TestMethod]
	public void FormsShouldPrint90PercentApplication()
	{
		var generator = _sut.Create("90 percent application", _dbContext, 1, 2, 1);
		Assert.IsNotNull(generator);
		Assert.IsInstanceOfType<DefaultPrinter>(generator);

		if (!Directory.Exists(PdfOutputFolderPath))
			Directory.CreateDirectory(PdfOutputFolderPath);

		var request = new DocumentGenerationRequest("90 percent application", PdfOutputFolderPath);

		var response = generator.PrintDocument(request);
		Assert.IsNotNull(response);
		WriteLine(JsonConvert.SerializeObject(response));
	}

	[TestMethod]
	public void Form90PercentShouldHaveTheRadiusKeyValuePairs()
	{
		var generator = new KeyValuePairGenerator(_dbContext, 1, 2, 1);

		generator.NinetyPercentRadiusLessThan100.Should().NotBeEmpty();
		generator.NinetyPercentRadius100To200.Should().NotBeEmpty();
		generator.NinetyPercentRadius200To500.Should().NotBeEmpty();
		generator.NinetyPercentRadius500Plus.Should().NotBeEmpty();
		generator.NinetyPercentRadius12WesternStates.Should().NotBeEmpty();
		generator.NinetyPercentRadiusUnlimited.Should().NotBeEmpty();

		generator.NinetyPercentRadiusLessThan100.Should().Be("0.01");
		generator.NinetyPercentRadius100To200.Should().Be("0.02");
		generator.NinetyPercentRadius200To500.Should().Be("0.03");
		generator.NinetyPercentRadius500Plus.Should().Be("0.04");
		generator.NinetyPercentRadius12WesternStates.Should().Be("0.05");
		generator.NinetyPercentRadiusUnlimited.Should().Be("0.06");
	}

	[DataTestMethod]
	[DataRow(true)]
	[DataRow(false)]
	public void CommoditiesFieldsShouldBePopulated(bool getClientFirst)
	{
		var keyValuePairGenerator = new KeyValuePairGenerator(_dbContext, 1, 2, 1);

		if (getClientFirst)
			GetFirstClient(_dbContext.AgencyClients);
		
		Assert.IsTrue(keyValuePairGenerator.CommoditiesFields.Count > 0, "commodities should not be empty");
		CollectionAssert.AllItemsAreNotNull(keyValuePairGenerator.CommoditiesFields, "commodities should not be null");
		CollectionAssert.AllItemsAreUnique(keyValuePairGenerator.CommoditiesFields, "commodities should be unique");
	}

	
	private void GetFirstClient(DbSet<AgencyClient> agencyClients)
	{
		var agencyClient = agencyClients
							.Include(x=>x.Commodities)
							.Single(x => x.AgencyClientId==1);

		_dbContext.Entry(agencyClient).Reference(x => x.Commodities).Load();
		var commodities = agencyClient.Commodities.ToArray();
		Assert.IsFalse(commodities.Length == 0);
	}


	[TestMethod]
	public void ClientCommoditiesShouldNotBeEmpty()
	{
		var client = _dbContext.AgencyClients
			.Where(x => x.AgencyClientId == 1)
			.Include(x => x.Commodities)
			.Single();

		Assert.IsNotNull(client.Commodities);
		Assert.IsTrue(client.Commodities.Count != 0, "Agencty Client with ID == 1 should have commodities");
	}


	[DataTestMethod]
	[DataRow("acord 50", typeof(Acord50Printer))]
	[DataRow("acord 127", typeof(Acord127Printer))]
	[DataRow("acord 125", typeof(DefaultPrinter))]
	[DataRow("acord 35", typeof(DefaultPrinter))]
	[DataRow("90 percent application", typeof(DefaultPrinter))]
	public void FormsShouldPrint(string formName, Type expectedPrinterType)
	{
		var generator = _sut.Create(formName, _dbContext, 1, 2, 1);
		Assert.IsNotNull(generator);
		Assert.IsInstanceOfType(generator, expectedPrinterType);

		if (!Directory.Exists(PdfOutputFolderPath))
			Directory.CreateDirectory(PdfOutputFolderPath);

		var request = new DocumentGenerationRequest(formName, PdfOutputFolderPath);

		var response = generator.PrintDocument(request);
		Assert.IsNotNull(response);
		WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));

		IReadOnlyDictionary<string, GeneratedDocument> generatedDocuments = response;

		foreach (var key in generatedDocuments.Keys)
		{
			var document = generatedDocuments[key];

			var stringBuilder = new StringBuilder("\n")
				.AppendLine(new string('#', 120))
				.AppendLine($"form name: {document.FormName}")
				.AppendLine($"description: {document.Description}")
				.AppendLine(document.FilePath);

			WriteLine(stringBuilder.ToString());
		}
	}



	[DataTestMethod]
	[DataRow("acord 50", typeof(Acord50Printer))]
	[DataRow("acord.50", typeof(Acord50Printer))]
	[DataRow("acord_50", typeof(Acord50Printer))]
	[DataRow("acord50", typeof(Acord50Printer))]
	[DataRow("50", typeof(Acord50Printer))]
	[DataRow("ACORD 50", typeof(Acord50Printer))]
	[DataRow("ACORD.50", typeof(Acord50Printer))]
	[DataRow("ACORD_50", typeof(Acord50Printer))]
	[DataRow("ACORD50", typeof(Acord50Printer))]
	[DataRow("acord 127", typeof(Acord127Printer))]
	[DataRow("acord.127", typeof(Acord127Printer))]
	[DataRow("acord_127", typeof(Acord127Printer))]
	[DataRow("acord127", typeof(Acord127Printer))]
	[DataRow("ACORD 127", typeof(Acord127Printer))]
	[DataRow("ACORD.127", typeof(Acord127Printer))]
	[DataRow("ACORD_127", typeof(Acord127Printer))]
	[DataRow("ACORD127", typeof(Acord127Printer))]
	[DataRow("127", typeof(Acord127Printer))]
	[DataRow("acord 25", typeof(DefaultPrinter))]
	[DataRow("acord 35", typeof(DefaultPrinter))]
	[DataRow("acord 36", typeof(DefaultPrinter))]
	[DataRow("acord 125", typeof(DefaultPrinter))]
	[DataRow("acord 126", typeof(DefaultPrinter))]
	[DataRow("acord 129", typeof(DefaultPrinter))]
	[DataRow("acord 131", typeof(DefaultPrinter))]
	[DataRow("acord 137", typeof(DefaultPrinter))]
	[DataRow("acord 163", typeof(DefaultPrinter))]
	[DataRow("acord.25", typeof(DefaultPrinter))]
	[DataRow("acord.35", typeof(DefaultPrinter))]
	[DataRow("acord.36", typeof(DefaultPrinter))]
	[DataRow("acord.125", typeof(DefaultPrinter))]
	[DataRow("acord.126", typeof(DefaultPrinter))]
	[DataRow("acord.129", typeof(DefaultPrinter))]
	[DataRow("acord.131", typeof(DefaultPrinter))]
	[DataRow("acord.137", typeof(DefaultPrinter))]
	[DataRow("acord.163", typeof(DefaultPrinter))]
	[DataRow("acord_25", typeof(DefaultPrinter))]
	[DataRow("acord_35", typeof(DefaultPrinter))]
	[DataRow("acord_36", typeof(DefaultPrinter))]
	[DataRow("acord_125", typeof(DefaultPrinter))]
	[DataRow("acord_126", typeof(DefaultPrinter))]
	[DataRow("acord_129", typeof(DefaultPrinter))]
	[DataRow("acord_131", typeof(DefaultPrinter))]
	[DataRow("acord_137", typeof(DefaultPrinter))]
	[DataRow("acord_163", typeof(DefaultPrinter))]
	[DataRow("acord-25", typeof(DefaultPrinter))]
	[DataRow("acord-35", typeof(DefaultPrinter))]
	[DataRow("acord-36", typeof(DefaultPrinter))]
	[DataRow("acord-125", typeof(DefaultPrinter))]
	[DataRow("acord-126", typeof(DefaultPrinter))]
	[DataRow("acord-129", typeof(DefaultPrinter))]
	[DataRow("acord-131", typeof(DefaultPrinter))]
	[DataRow("acord-137", typeof(DefaultPrinter))]
	[DataRow("acord-163", typeof(DefaultPrinter))]
	[DataRow("acord25", typeof(DefaultPrinter))]
	[DataRow("acord35", typeof(DefaultPrinter))]
	[DataRow("acord36", typeof(DefaultPrinter))]
	[DataRow("acord125", typeof(DefaultPrinter))]
	[DataRow("acord126", typeof(DefaultPrinter))]
	[DataRow("acord129", typeof(DefaultPrinter))]
	[DataRow("acord131", typeof(DefaultPrinter))]
	[DataRow("acord137", typeof(DefaultPrinter))]
	[DataRow("acord163", typeof(DefaultPrinter))]
	[DataRow("ACORD 25", typeof(DefaultPrinter))]
	[DataRow("ACORD 35", typeof(DefaultPrinter))]
	[DataRow("ACORD 36", typeof(DefaultPrinter))]
	[DataRow("ACORD 125", typeof(DefaultPrinter))]
	[DataRow("ACORD 126", typeof(DefaultPrinter))]
	[DataRow("ACORD 129", typeof(DefaultPrinter))]
	[DataRow("ACORD 131", typeof(DefaultPrinter))]
	[DataRow("ACORD 137", typeof(DefaultPrinter))]
	[DataRow("ACORD 163", typeof(DefaultPrinter))]
	[DataRow("ACORD.25", typeof(DefaultPrinter))]
	[DataRow("ACORD.35", typeof(DefaultPrinter))]
	[DataRow("ACORD.36", typeof(DefaultPrinter))]
	[DataRow("ACORD.125", typeof(DefaultPrinter))]
	[DataRow("ACORD.126", typeof(DefaultPrinter))]
	[DataRow("ACORD.129", typeof(DefaultPrinter))]
	[DataRow("ACORD.131", typeof(DefaultPrinter))]
	[DataRow("ACORD.137", typeof(DefaultPrinter))]
	[DataRow("ACORD.163", typeof(DefaultPrinter))]
	[DataRow("ACORD_25", typeof(DefaultPrinter))]
	[DataRow("ACORD_35", typeof(DefaultPrinter))]
	[DataRow("ACORD_36", typeof(DefaultPrinter))]
	[DataRow("ACORD_125", typeof(DefaultPrinter))]
	[DataRow("ACORD_126", typeof(DefaultPrinter))]
	[DataRow("ACORD_129", typeof(DefaultPrinter))]
	[DataRow("ACORD_131", typeof(DefaultPrinter))]
	[DataRow("ACORD_137", typeof(DefaultPrinter))]
	[DataRow("ACORD_163", typeof(DefaultPrinter))]
	[DataRow("ACORD-25", typeof(DefaultPrinter))]
	[DataRow("ACORD-35", typeof(DefaultPrinter))]
	[DataRow("ACORD-36", typeof(DefaultPrinter))]
	[DataRow("ACORD-125", typeof(DefaultPrinter))]
	[DataRow("ACORD-126", typeof(DefaultPrinter))]
	[DataRow("ACORD-129", typeof(DefaultPrinter))]
	[DataRow("ACORD-131", typeof(DefaultPrinter))]
	[DataRow("ACORD-137", typeof(DefaultPrinter))]
	[DataRow("ACORD-163", typeof(DefaultPrinter))]
	[DataRow("ACORD25", typeof(DefaultPrinter))]
	[DataRow("ACORD35", typeof(DefaultPrinter))]
	[DataRow("ACORD36", typeof(DefaultPrinter))]
	[DataRow("ACORD125", typeof(DefaultPrinter))]
	[DataRow("ACORD126", typeof(DefaultPrinter))]
	[DataRow("ACORD129", typeof(DefaultPrinter))]
	[DataRow("ACORD131", typeof(DefaultPrinter))]
	[DataRow("ACORD137", typeof(DefaultPrinter))]
	[DataRow("ACORD163", typeof(DefaultPrinter))]
	[DataRow("25", typeof(DefaultPrinter))]
	[DataRow("35", typeof(DefaultPrinter))]
	[DataRow("36", typeof(DefaultPrinter))]
	[DataRow("125", typeof(DefaultPrinter))]
	[DataRow("126", typeof(DefaultPrinter))]
	[DataRow("129", typeof(DefaultPrinter))]
	[DataRow("131", typeof(DefaultPrinter))]
	[DataRow("137", typeof(DefaultPrinter))]
	[DataRow("163", typeof(DefaultPrinter))]
	[DataRow("Supplemental Operator Schedule", typeof(DefaultPrinter))]
	public void FactoryShouldGenerateCorrectObjectTypes(string documentType, Type expectedType)
	{
		var generator = _sut.Create(documentType, _dbContext, 1, 2, 1);
		Assert.IsNotNull(generator);
		Assert.IsInstanceOfType(generator, expectedType);
	}


	[DataTestMethod]
	[DataRow("acord 25")]
	[DataRow("acord 35")]
	[DataRow("acord 36")]
	[DataRow("acord 37")]
	[DataRow("acord 50")]
	[DataRow("acord 125")]
	[DataRow("acord 126")]
	[DataRow("acord 127")]
	[DataRow("acord 129")]
	[DataRow("acord 131")]
	[DataRow("acord 137")]
	[DataRow("acord 163")]
	[DataRow("Supplemental Operator Schedule")]
	public void AllFormsShouldHaveFactories(string documentName)
	{
		var generator = _sut.Create(documentName, _dbContext, 1, 2, 1);
		Assert.IsNotNull(generator);
		Assert.IsInstanceOfType(generator, typeof(object));
	}


	[TestMethod]
	public void KeyValuePairGeneratorContructorShouldNotThrowExceptions()
	{
		var generator = new KeyValuePairGenerator(_dbContext, 1, 2, 1, null);
		var pairs = generator.GetPairs();
		Assert.IsTrue(pairs.Any());
	}


	public void Dispose() => _dbContext?.Dispose();
}
