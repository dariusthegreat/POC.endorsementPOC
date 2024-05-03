using Microsoft.Extensions.Configuration;
using FluentAssertions;
using TechQ.Entities.Models;
using TechQ.MgaFiltering.Predicates.Factories;

namespace TechQ.MgaFiltering.Tests;

//[TestClass]
public class FilterUnitTests
{
	private readonly MgaFilterFactory _mgaFilterFactory;

	private readonly InsuranceDbContext _dbContext;

	private static readonly Lazy<IDictionary<string, DatabaseConnectionDetails>> s_lazyLoadedDatabaseConnectionDetails = new(GetDatabaseConnections);
	private static readonly Lazy<IConfigurationRoot> s_lazyLoadedConfiguration = new(LoadConfiguration);

	private static IConfigurationRoot Configuration => s_lazyLoadedConfiguration.Value;
	private static IDictionary<string, DatabaseConnectionDetails> DatabaseConnections => s_lazyLoadedDatabaseConnectionDetails.Value;


	public TestContext TestContext { get; set; }


	private static IConfigurationRoot LoadConfiguration() => new ConfigurationBuilder()
															 .SetBasePath(Directory.GetCurrentDirectory())
															 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
															 .Build();


	private static Dictionary<string, DatabaseConnectionDetails> GetDatabaseConnections() => Configuration.GetSection("dbConnections")
																											.Get<Dictionary<string, DatabaseConnectionDetails>>()
																											.ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);


	public FilterUnitTests()
	{
		_dbContext = new InsuranceDbContext(DatabaseConnections["InsuranceDb"]);
		_mgaFilterFactory = new(_dbContext);
	}


	private void WriteLine(string message) => TestContext.WriteLine(message);



    [TestMethod]
	public void FilterShouldReturnCorrectMgas()
	{
		var mgaPredicates = MgaFilterFactory.CreateAll(_dbContext);
		var application   = GetApplication();

		var results = mgaPredicates.ToDictionary(x => x, x => x.Test(application));

		results.Should().NotBeNull();
		results.Count.Should().Be(mgaPredicates.Length);

		foreach (var mgaPredicate in results.Keys)
		{
			WriteLine("\n");
			WriteLine($"MGA: {mgaPredicate.MgaName}");
			var mgaResults = results[mgaPredicate];

			foreach (var insuranceCompanyResult in mgaResults)
			{
				WriteLine(new string('-', 80));
				WriteLine($"insurance company: {insuranceCompanyResult.InsuranceCompany.InsuranceCoName}");

				foreach (var criterion in insuranceCompanyResult)
				{
					WriteLine($"{criterion.Key}: {criterion.Value}");
				}
			}
		}
	}

	private Application GetApplication() => throw new NotImplementedException();
}