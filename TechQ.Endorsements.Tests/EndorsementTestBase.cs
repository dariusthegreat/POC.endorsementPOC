using Microsoft.Extensions.Configuration;
using TechQ.Entities.Models;

namespace TechQ.Endorsements.Tests;

public abstract class EndorsementTestBase
{
	protected readonly InsuranceDbContext _dbContext = new(DatabaseConnections["InsuranceDb"]);
	
	private static readonly Lazy<IDictionary<string, Core.DatabaseConnectionDetails>> s_lazyLoadedDatabaseConnectionDetails = new(GetDatabaseConnections);
	private static readonly Lazy<IConfigurationRoot> s_lazyLoadedConfiguration = new(LoadConfiguration);

	private static IConfigurationRoot Configuration => s_lazyLoadedConfiguration.Value;
	private static IDictionary<string, Core.DatabaseConnectionDetails> DatabaseConnections => s_lazyLoadedDatabaseConnectionDetails.Value;


	// ReSharper disable once UnusedAutoPropertyAccessor.Global
	public TestContext TestContext { get; set; }

	private static IConfigurationRoot LoadConfiguration() => new ConfigurationBuilder()
	                                                         .SetBasePath(Directory.GetCurrentDirectory())
	                                                         .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
	                                                         .Build();


	private static Dictionary<string, Core.DatabaseConnectionDetails> GetDatabaseConnections() => Configuration.GetSection("dbConnections")
	                                                                                                           .Get<Dictionary<string, Core.DatabaseConnectionDetails>>()
	                                                                                                           .ToDictionary(x => x.Key, x => x.Value, StringComparer.OrdinalIgnoreCase);


	protected void WriteLine(string message) => TestContext.WriteLine(message);
}