using ImpromptuInterface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Poc.Library.Models;

namespace Poc.Library;

public class DbContextSerializer
{
    public Agency[] Agencies { get; set; }
    public AgencyAgent[] AgencyAgents { get; set; }
    public AgencyClient[] AgencyClients { get; set; }
    public Application[] Applications { get; set; }
    public CoverageRequest[] CoverageRequests { get; set; }
    public Insurer[] Insurers { get; set; }
    public Quote[] Quotes { get; set; }
    public Driver[] Drivers { get; set; }
    public Garage[] Garages { get; set; }
    public Vehicle[] Vehicles { get; set; }


    public DbContextSerializer() { }

    public DbContextSerializer(Agency[] agencies,
                                AgencyAgent[] agencyAgents,
                                AgencyClient[] agencyClients,
                                Application[] applications,
                                CoverageRequest[] coverageRequests,
                                Insurer[] insurers,
                                Quote[] quotes,
                                Driver[] drivers,
                                Garage[] garages,
                                Vehicle[] vehicles)
    {
        Agencies = agencies;
        AgencyAgents = agencyAgents;
        AgencyClients = agencyClients;
        Applications = applications;
        CoverageRequests = coverageRequests;
        Insurers = insurers;
        Quotes = quotes;
        Drivers = drivers;
        Garages = garages;
        Vehicles = vehicles;
    }

    public DbContextSerializer(InsuranceDbContext dbContext) : this(dbContext.Agencies.ToArray(),
                                                                    dbContext.AgencyAgents.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.AgencyClients.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.Applications.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.CoverageRequests.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.Insurers.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.Quotes.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.Drivers.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.Garages.Select(x => Detach(dbContext, x)).ToArray(),
                                                                    dbContext.Vehicles.Select(x => Detach(dbContext, x)).ToArray())
    { }

    private static T Detach<T>(InsuranceDbContext dbContext, T entity) where T : class
    {
        dbContext.Entry(entity).State = EntityState.Detached;
        return entity;
    }


    public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });


    public static DbContextSerializer FromString(string json) => JsonConvert.DeserializeObject<DbContextSerializer>(json);
}


public static class DbContextStubFactory
{
    public static IInsuranceDbContext Create(string jsonFilePath)
    {
        var serializer = JsonConvert.DeserializeObject<DbContextSerializer>(File.ReadAllText(jsonFilePath));

        var count = serializer.AgencyClients.Count(x => x.AgencyClientId == 1);
        Console.Out.WriteLine($"serializer agency clients with client ID ==1: {count}");

        dynamic adapter = new DbContextAdapter(serializer);

        var stub = ((object)adapter).ActLike<IInsuranceDbContext>();

        var countInStub = stub.AgencyClients.Count(x => x.AgencyClientId == 1);
        Console.Out.WriteLine($"strub context agency clients with client ID ==1: {count}");

        return stub;
    }
}