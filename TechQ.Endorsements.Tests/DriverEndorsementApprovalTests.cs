using TechQ.Core.Extensions;
using TechQ.Entities.Models;

namespace TechQ.Endorsements.Tests;

//[TestClass]
public class DriverEndorsementApprovalTests : EndorsementTestBase
{
	private readonly DriverEndorsementManager _sut;

	public DriverEndorsementApprovalTests()
	{
		_sut = new DriverEndorsementManager(_dbContext);
	}


	[TestMethod]
	public async Task CommitInsert()
	{
		using CancellationTokenSource cancellationTokenSource = new();

		var driverEndorsement = new DriverEndorsement
		                        {
			                        AgencyClientId = 1,
			                        DriverDob = new DateOnly(1975, 1, 1),
			                        DriverExcluded = "N",
			                        DriverFirstName = "First",
			                        DriverMiddleName = "Middle",
			                        DriverLastName = "Last",
			                        DriverExperienceStartDate = DateTimeOffset.Now.AddMonths(-(5 * 12 + 6)).ToDateOnly(),
			                        DriverHireDate = DateTimeOffset.Now.AddMonths(-(3 * 12)).ToDateOnly(),
			                        DriverLicenseClass = "B",
			                        DriverLicenseNumber = "1234",
			                        DriverLicenseState = "CA",
			                        DriverType = "anxious?",
			                        EndorsementStatusDate = DateTimeOffset.Now.ToString(),
			                        EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly(),
			                        EndorsementStatus = "insert",
			                        OperationType = "insert"
		                        };

		var endorsement = await _sut.InsertAsync(driverEndorsement, cancellationTokenSource.Token);

		await _sut.CommitAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated to show it's been processed (inserted)
		// - ensure the driver record has been inserted into the DB table
	}

	[TestMethod]
	public async Task CommitUpdate()
	{
		using CancellationTokenSource cancellationTokenSource = new();
		var                           driver = _dbContext.Drivers.GetRandomRecord();
		var                           endorsement = await _sut.UpdateAsync(driver, cancellationTokenSource.Token);
		await _sut.CommitAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated to show it's been processed (updated)
		// - ensure the driver record in the driver table has been updated
		// - ensure the original record values are in the endorsement table
	}

	[TestMethod]
	public async Task CommitDelete()
	{
		using CancellationTokenSource cancellationTokenSource = new();
		var driver = _dbContext.Drivers.GetRandomRecord();
		var endorsement = await _sut.DeleteAsync(driver.DriverId, cancellationTokenSource.Token);
		await _sut.CommitAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated to show it's been processed (deleted)
		// - ensure the driver record has been deleted from the driver table
	}
}