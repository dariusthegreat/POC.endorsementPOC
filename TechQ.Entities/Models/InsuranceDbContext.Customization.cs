using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TechQ.Entities.Models;

public partial class InsuranceDbContext : IInsuranceDbContext
{
    private readonly string _connectionString;

    public InsuranceDbContext(IConfigurationManager configurationManager) : this(configurationManager.GetConnectionString("InsuranceDb")) { }

    public InsuranceDbContext(string connectionString)
    {
		ArgumentNullException.ThrowIfNull(connectionString);
		if (connectionString == "") throw new ArgumentException($"invalid (empty) '{nameof(connectionString)}'.", nameof(connectionString));
        if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or whitespace.", nameof(connectionString));

        _connectionString = connectionString;
    }

    private string ConnectionString => _connectionString;
    
    IEnumerable<Agency>                     IInsuranceDbContext.Agencies         => Agencies                  ;
    IEnumerable<AgencyAgent>                IInsuranceDbContext.AgencyAgents     => AgencyAgents              ;
    IQueryable<AgencyClient>                IInsuranceDbContext.AgencyClients    => AgencyClients             ;
    IEnumerable<Application>                IInsuranceDbContext.Applications     => Applications              ;
    IEnumerable<CoverageRequest>            IInsuranceDbContext.CoverageRequests => CoverageRequests          ;
    IEnumerable<Insurer>                    IInsuranceDbContext.Insurers         => Insurers                  ;
    IEnumerable<Quote>                      IInsuranceDbContext.Quotes           => Quotes                    ;
    IEnumerable<Driver>                     IInsuranceDbContext.Drivers          => Drivers                   ;
    IEnumerable<Garage>                     IInsuranceDbContext.Garages          => Garages                   ;
    IEnumerable<Vehicle>                    IInsuranceDbContext.Vehicles         => Vehicles                  ;
    IEnumerable<Policy>                     IInsuranceDbContext.Policies         => Policies                  ;
    IEnumerable<Market>                     IInsuranceDbContext.Markets          => Markets                   ;
    IEnumerable<Insurance>                  IInsuranceDbContext.Insurances       => Insurances                ;
    IQueryable<FilteringInsurerCriterion>   IInsuranceDbContext.MgaCriterion     => FilteringInsurerCriteria  ;
}

