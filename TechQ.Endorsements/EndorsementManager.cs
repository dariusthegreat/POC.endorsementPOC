using TechQ.Entities.Models;

namespace TechQ.Endorsements;

// chat URL: https://mytechqworkspace.slack.com/archives/D06KQNFH3QV/p1711762310245279

public class EndorsementManager(InsuranceDbContext dbContext)
{
	private readonly InsuranceDbContext        _dbContext                 = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
	private readonly DriverEndorsementManager  _driverEndorsementManager  = new(dbContext);
	private readonly VehicleEndorsementManager _vehicleEndorsementManager = new(dbContext);

	public DriverEndorsementManager  Drivers  => _driverEndorsementManager;
	public VehicleEndorsementManager Vehicles => _vehicleEndorsementManager;
}