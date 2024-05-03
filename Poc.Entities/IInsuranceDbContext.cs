using Poc.Entities.Models;

namespace Poc.Entities;
public interface IInsuranceDbContext
{
	IQueryable<AgencyClient>	 AgencyClients    { get; }
	IEnumerable<Agency>          Agencies         { get; }
	IEnumerable<AgencyAgent>     AgencyAgents     { get; }
	IEnumerable<Application>     Applications     { get; }
	IEnumerable<Insurer>         Insurers         { get; }
	IEnumerable<Quote>           Quotes           { get; }
	IEnumerable<CoverageRequest> CoverageRequests { get; }
	IEnumerable<Driver>          Drivers          { get; }
	IEnumerable<Vehicle>         Vehicles         { get; }
	IEnumerable<Garage>          Garages          { get; }
	IEnumerable<Policy>			 Policies		  { get; }
	IEnumerable<Market>			 Markets		  { get; }
	IEnumerable<Insurance>		 Insurances		  { get; }
}
