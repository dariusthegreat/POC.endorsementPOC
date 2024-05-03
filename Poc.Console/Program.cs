using System.Configuration;
using ConsoleDump;
using Poc.Console;
using TechQ.Entities.Models;
using TechQ.Core.Extensions;
using TechQ.DocumentManagement;
using TechQ.DocumentManagement.PdfPopulation;


// todo: remove this line once the application is running from a deployed location and pass the arguments to the application instance instead.
args = ["forms", @"forms\populated"];

if (args?.Length != 2)
{
	Console.WriteLine("Usage: Poc.Console.exe <source folder path> <destination folder path>");
	return -1;
}

var (sourceFolderPath, destinationFolderPath) = args.Dump("folder paths (read from application arguments)");


using var dbContext = new InsuranceDbContext(ConfigurationManager.ConnectionStrings["InsuranceDb"].ConnectionString);
//var dbContext = new InsuranceDbDummyDataGenerator().GetDbContext();

var sessionId = SessionHelpers.GetSessionId();

var fieldValues = new KeyValuePairGenerator(dbContext, 1, 2, 1).KeyValuePairs.DumpKeyValuePairs()
																			.DumpKeyValuePairsToJsonFile(sessionId, destinationFolderPath)
																			.DumpKeyValuePairsToTextFile(sessionId, destinationFolderPath);


#if false

new MainWorker(new AsposePdfPopulator(), fieldValues) { SessionId = sessionId }.PopulateAllPdfDocuments(sourceFolderPath, destinationFolderPath, "25");

#else

new MainWorker(new AsposePdfPopulator(), fieldValues) { SessionId = sessionId}.PopulateAllPdfDocuments(sourceFolderPath, destinationFolderPath);

#endif


return 0;

