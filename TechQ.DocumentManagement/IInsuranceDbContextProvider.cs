using TechQ.Entities;

namespace TechQ.DocumentManagement;

public interface IInsuranceDbContextProvider
{
	long   ClientId             { get; set; }
	long	AgencyId			{ get; set; }
	long	AgentId				{ get; set; }
	long?	VehicleId			{ get; set; }
	int    CoverageRequestCount { get; set; }
	int    QuotesCount          { get; set; }
	int    InsurersCount        { get; set; }
	int    DriverCount          { get; set; }
	int    GaragesCount         { get; set; }
	int    VehiclesCount        { get; set; }

	IInsuranceDbContext GetDbContext();
}
