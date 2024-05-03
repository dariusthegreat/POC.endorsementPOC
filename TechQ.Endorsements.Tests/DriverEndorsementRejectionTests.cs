using TechQ.Core.Extensions;
using TechQ.Entities.Models;

namespace TechQ.Endorsements.Tests;

//[TestClass]
public class DriverEndorsementRejectionTests : EndorsementTestBase
{
	private readonly DriverEndorsementManager _sut;

	public DriverEndorsementRejectionTests()
	{
		_sut = new DriverEndorsementManager(_dbContext);
	}

	[TestMethod]
	public async Task RejectInsert()
	{
		using CancellationTokenSource cancellationTokenSource = new();

		var driverEndorsement = new DriverEndorsement
		                        {
			                        AgencyClientId            = 1,
			                        DriverDob                 = new DateOnly(1975, 1, 1),
			                        DriverExcluded            = "N",
			                        DriverFirstName           = "First",
			                        DriverMiddleName          = "Middle",
			                        DriverLastName            = "Last",
			                        DriverExperienceStartDate = DateTimeOffset.Now.AddMonths(-(5 * 12 + 6)).ToDateOnly(),
			                        DriverHireDate            = DateTimeOffset.Now.AddMonths(-(3 * 12)).ToDateOnly(),
			                        DriverLicenseClass        = "B",
			                        DriverLicenseNumber       = "1234",
			                        DriverLicenseState        = "CA",
			                        DriverType                = "anxious?",
			                        EndorsementStatusDate     = DateTimeOffset.Now.ToString(),
			                        EndorsementRequestDate    = DateTimeOffset.Now.ToDateOnly(),
			                        EndorsementStatus         = "insert",
			                        OperationType             = "insert"
		                        };

		var endorsement = await _sut.InsertAsync(driverEndorsement, cancellationTokenSource.Token);

		await _sut.WithdrawAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated
		// - ensure the driver record is NOT inserted into the DB table
	}

	[TestMethod]
	public async Task RejectUpdate()
	{
		using CancellationTokenSource cancellationTokenSource = new();
		var                           driver                  = _dbContext.Drivers.GetRandomRecord();
		var                           endorsement             = await _sut.UpdateAsync(driver, cancellationTokenSource.Token);
		await _sut.WithdrawAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated to show it's been withdrawn
		// - ensure the driver record in the driver table is NOT modified
	}

	[TestMethod]
	public async Task RejectDelete()
	{
		using CancellationTokenSource cancellationTokenSource = new();
		var                           driver                  = _dbContext.Drivers.GetRandomRecord();
		var                           endorsement             = await _sut.DeleteAsync(driver.DriverId, cancellationTokenSource.Token);
		await _sut.WithdrawAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated to show it's been withdrawn
		// - ensure the driver record does NOT get deleted from the driver table
	}
}