using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using TechQ.Core.Extensions;
using TechQ.Entities.Models;

namespace TechQ.Endorsements.Tests;

//[TestClass]
public class DriverEndorsementTests : EndorsementTestBase
{
	private readonly DriverEndorsementManager _sut;

	public DriverEndorsementTests()
	{
		_sut = new(_dbContext);
	}


	[TestMethod]
	public void TestDriverToDriverEndorsementMapping()
	{
		var driver = new Driver
		             {
			             DriverId                  = 42,
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
			             DriverType                = "anxious?"
		             };

		var endorsement = (DriverEndorsement) driver;

		endorsement.Should().NotBe(null);
		endorsement.AgencyClientId.Should().Be(driver.AgencyClientId);
		endorsement.DriverId.Should().Be(driver.DriverId);
		endorsement.DriverDob.Should().Be(driver.DriverDob);
		endorsement.DriverExcluded.Should().Be(driver.DriverExcluded);
		endorsement.DriverFirstName.Should().Be(driver.DriverFirstName);
		endorsement.DriverMiddleName.Should().Be(driver.DriverMiddleName);
		endorsement.DriverLastName.Should().Be(driver.DriverLastName);
		endorsement.DriverExperienceStartDate.Should().Be(driver.DriverExperienceStartDate);
		endorsement.DriverHireDate.Should().Be(driver.DriverHireDate);
		endorsement.DriverLicenseClass.Should().Be(driver.DriverLicenseClass);
		endorsement.DriverLicenseNumber.Should().Be(driver.DriverLicenseNumber);
		endorsement.DriverLicenseState.Should().Be(driver.DriverLicenseState);
		endorsement.DriverType.Should().Be(driver.DriverType);
	}

	[TestMethod]
	public void TestDriverEndorsementToDriverMapping()
	{
		var endorsement = new DriverEndorsement
		                  {
			                  AgencyClientId            = 1,
			                  DriverDob                 = new DateOnly(1975, 1, 1),
			                  DriverExcluded            = "no",
			                  DriverFirstName           = "First",
			                  DriverMiddleName          = "Middle",
			                  DriverLastName            = "Last",
			                  DriverExperienceStartDate = DateTimeOffset.Now.AddMonths(-(5 * 12 + 6)).ToDateOnly(),
			                  DriverHireDate            = DateTimeOffset.Now.AddMonths(-(3 * 12)).ToDateOnly(),
			                  DriverLicenseClass        = "B",
			                  DriverLicenseNumber       = "1234",
			                  DriverLicenseState        = "CA",
			                  DriverType                = "anxious?",
			                  DriverId                  = 42,
			                  OperationType             = "operation type",
			                  EndorsementId             = 1,
			                  EndorsementRequestDate    = DateTimeOffset.Now.ToDateOnly(),
			                  EndorsementStatus         = "status",
			                  EndorsementStatusDate     = DateTimeOffset.Now.ToString(),
			                  Id                        = 3,
			                  PolicyChangeEffectiveDate = DateTimeOffset.Now.ToDateOnly()
		                  };

		var driver = (Driver) endorsement;

		driver.Should().NotBe(null);
		driver.AgencyClientId.Should().Be(endorsement.AgencyClientId);
		driver.DriverDob.Should().Be(endorsement.DriverDob);
		driver.DriverExcluded.Should().Be(endorsement.DriverExcluded);
		driver.DriverFirstName.Should().Be(endorsement.DriverFirstName);
		driver.DriverMiddleName.Should().Be(endorsement.DriverMiddleName);
		driver.DriverLastName.Should().Be(endorsement.DriverLastName);
		driver.DriverExperienceStartDate.Should().Be(endorsement.DriverExperienceStartDate);
		driver.DriverHireDate.Should().Be(endorsement.DriverHireDate);
		driver.DriverLicenseClass.Should().Be(endorsement.DriverLicenseClass);
		driver.DriverLicenseNumber.Should().Be(endorsement.DriverLicenseNumber);
		driver.DriverLicenseState.Should().Be(endorsement.DriverLicenseState);
		driver.DriverType.Should().Be(endorsement.DriverType);
		driver.DriverId.Should().Be(endorsement.DriverId);
	}


	[TestMethod]
	public async Task Insert()
	{
		using CancellationTokenSource cancellationTokenSource = new();

		var driverLicenseNumber = Guid.NewGuid().ToString("N");

		var driverEndorsement = new DriverEndorsement
		                        {
			                        AgencyClientId   = 1,
			                        DriverDob        = new DateOnly(1975, 1, 1),
			                        DriverExcluded   = "N",
			                        DriverFirstName  = "First",
			                        DriverMiddleName = "Middle",
			                        DriverLastName   = "Last",
			                        DriverExperienceStartDate = DateTimeOffset.Now.AddMonths(-(5 * 12 + 6)).ToDateOnly(),
			                        DriverHireDate         = DateTimeOffset.Now.AddMonths(-(3 * 12)).ToDateOnly(),
			                        DriverLicenseClass     = "B",
			                        DriverLicenseNumber    = driverLicenseNumber,
			                        DriverLicenseState     = "CA",
			                        DriverType             = "anxious?",
			                        EndorsementStatusDate  = DateTimeOffset.Now.ToString(),
			                        EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly(),
			                        EndorsementStatus      = "insert",
			                        OperationType          = "insert"
		                        };

		var endorsement = await _sut.InsertAsync(driverEndorsement, cancellationTokenSource.Token);
			
		var record = await _dbContext.DriverEndorsements.SingleOrDefaultAsync(x => x.DriverLicenseNumber== driverLicenseNumber, cancellationTokenSource.Token);
		record.Should().NotBe(null);
		record.AgencyClientId.Should().Be(1);
		record.DriverDob.Should().Be(driverEndorsement.DriverDob);
		record.DriverExcluded.Should().Be(driverEndorsement.DriverExcluded);
		record.DriverFirstName.Should().Be(driverEndorsement.DriverFirstName);
		record.DriverMiddleName.Should().Be(driverEndorsement.DriverMiddleName);
		record.DriverLastName.Should().Be(driverEndorsement.DriverLastName);
		record.DriverExperienceStartDate.Should().Be(driverEndorsement.DriverExperienceStartDate);
		record.DriverHireDate.Should().Be(driverEndorsement.DriverHireDate);
		record.DriverLicenseState.Should().Be(driverEndorsement.DriverLicenseState);
		record.DriverType.Should().Be(driverEndorsement.DriverType);
		record.EndorsementStatus.Should().Be("insert");
		record.OperationType.Should().Be("insert");

		record.AgencyClientId.Should().Be(endorsement.AgencyClientId);
		record.DriverDob.Should().Be(endorsement.DriverDob);
		record.DriverExcluded.Should().Be(endorsement.DriverExcluded);
		record.DriverFirstName.Should().Be(endorsement.DriverFirstName);
		record.DriverMiddleName.Should().Be(endorsement.DriverMiddleName);
		record.DriverLastName.Should().Be(endorsement.DriverLastName);
		record.DriverExperienceStartDate.Should().Be(endorsement.DriverExperienceStartDate);
		record.DriverHireDate.Should().Be(endorsement.DriverHireDate);
		record.DriverLicenseState.Should().Be(endorsement.DriverLicenseState);
		record.DriverType.Should().Be(endorsement.DriverType);
		record.EndorsementStatus.Should().Be(endorsement.EndorsementStatus);
		record.OperationType.Should().Be(endorsement.OperationType);
	}

	[TestMethod]
	public async Task Update()
	{
		using CancellationTokenSource cancellationTokenSource = new();

		var driver      = _dbContext.Drivers.GetRandomRecord();
		var endorsement = (DriverEndorsement) driver;
		endorsement.DriverFirstName  = $"{driver.DriverFirstName}-updated";
		endorsement.DriverMiddleName = $"{driver.DriverMiddleName}-updated";
		endorsement.DriverLastName   = $"{driver.DriverLastName}-updated";

		var updateEndorsementRecord = await _sut.UpdateAsync(endorsement, cancellationTokenSource.Token);

		var created = await _dbContext.DriverEndorsements.SingleOrDefaultAsync(x => x.DriverId==driver.DriverId, cancellationTokenSource.Token);
		created.Should().NotBe(null);
		created.AgencyClientId.Should().Be(driver.AgencyClientId);
		created.DriverDob.Should().Be(driver.DriverDob);
		created.DriverExcluded.Should().Be(driver.DriverExcluded);
		created.DriverFirstName.Should().Be(driver.DriverFirstName);
		created.DriverMiddleName.Should().Be(driver.DriverMiddleName);
		created.DriverLastName.Should().Be(driver.DriverLastName);
		created.DriverExperienceStartDate.Should().Be(driver.DriverExperienceStartDate);
		created.DriverHireDate.Should().Be(driver.DriverHireDate);
		created.DriverLicenseState.Should().Be(driver.DriverLicenseState);
		created.DriverType.Should().Be(driver.DriverType);
		created.EndorsementStatus.Should().Be("update");
		created.OperationType.Should().Be("update");

		created.AgencyClientId.Should().Be(updateEndorsementRecord.AgencyClientId);
		created.DriverDob.Should().Be(updateEndorsementRecord.DriverDob);
		created.DriverExcluded.Should().Be(updateEndorsementRecord.DriverExcluded);
		created.DriverFirstName.Should().Be(updateEndorsementRecord.DriverFirstName);
		created.DriverMiddleName.Should().Be(updateEndorsementRecord.DriverMiddleName);
		created.DriverLastName.Should().Be(updateEndorsementRecord.DriverLastName);
		created.DriverExperienceStartDate.Should().Be(updateEndorsementRecord.DriverExperienceStartDate);
		created.DriverHireDate.Should().Be(updateEndorsementRecord.DriverHireDate);
		created.DriverLicenseState.Should().Be(updateEndorsementRecord.DriverLicenseState);
		created.DriverType.Should().Be(updateEndorsementRecord.DriverType);
		created.EndorsementStatus.Should().Be(updateEndorsementRecord.EndorsementStatus);
		created.OperationType.Should().Be(updateEndorsementRecord.OperationType);
	}

	[TestMethod]
	public async Task Delete()
	{
		using CancellationTokenSource cancellationTokenSource = new();

		var driver      = _dbContext.Drivers.GetRandomRecord();
		var endorsement = await _sut.DeleteAsync(driver.DriverId, cancellationTokenSource.Token);

		var created = await _dbContext.DriverEndorsements.SingleOrDefaultAsync(x => x.DriverId == driver.DriverId, cancellationTokenSource.Token);
		created.Should().NotBe(null);
		created.AgencyClientId.Should().Be(endorsement.AgencyClientId);
		created.DriverDob.Should().Be(endorsement.DriverDob);
		created.DriverExcluded.Should().Be(endorsement.DriverExcluded);
		created.DriverFirstName.Should().Be(endorsement.DriverFirstName);
		created.DriverMiddleName.Should().Be(endorsement.DriverMiddleName);
		created.DriverLastName.Should().Be(endorsement.DriverLastName);
		created.DriverExperienceStartDate.Should().Be(endorsement.DriverExperienceStartDate);
		created.DriverHireDate.Should().Be(endorsement.DriverHireDate);
		created.DriverLicenseState.Should().Be(endorsement.DriverLicenseState);
		created.DriverType.Should().Be(endorsement.DriverType);
		created.EndorsementStatus.Should().Be("delete");
		created.OperationType.Should().Be("delete");
	}

	[TestMethod]
	public async Task ApproveDeletion()
	{
		using CancellationTokenSource cancellationTokenSource = new();

		// 1. create a new driver object and add it to the DB
		// 2. create a deletion endorsement
		// 3. approve the endorsement
		// 4. ensure the original driver record has been deleted
		// 5. ensure the endorsement record reflects it's already been approved

		// 1. create a new driver object and add it to the DB
		var driver = new Driver
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
			             DriverType                = "anxious?"
		             };

		driver.DriverId.Should().Be(default);

		await _dbContext.Drivers.AddAsync(driver, cancellationTokenSource.Token);
		await _dbContext.SaveChangesAsync(cancellationTokenSource.Token);

		driver.DriverId.Should().NotBe(default);

		//await _dbContext.Entry(driver).ReloadAsync(cancellationTokenSource.Token);

		// 2. create a deletion endorsement

		var endorsement = await _sut.DeleteAsync(driver.DriverId, cancellationTokenSource.Token);

		endorsement.Should().NotBeNull();
		endorsement.DriverId.Should().Be(driver.DriverId);
		endorsement.DriverFirstName.Should().Be(driver.DriverFirstName);
		endorsement.DriverMiddleName.Should().Be(driver.DriverMiddleName);
		endorsement.DriverLastName.Should().Be(driver.DriverLastName);
		endorsement.DriverLastName.Should().Be(driver.DriverLastName);
		endorsement.DriverHireDate.Should().Be(driver.DriverHireDate);
		endorsement.DriverLicenseClass.Should().Be(driver.DriverLicenseClass);
		endorsement.DriverLicenseState.Should().Be(driver.DriverLicenseState);
		endorsement.DriverLicenseNumber.Should().Be(driver.DriverLicenseNumber);
		endorsement.DriverType.Should().Be(driver.DriverType);
		endorsement.EndorsementStatus.Should().Be("delete");
		endorsement.OperationType.Should().Be("delete");

		(await _dbContext.Drivers.SingleOrDefaultAsync(x => x.DriverId == driver.DriverId, cancellationTokenSource.Token)).Should().NotBeNull();

		// 3. approve the endorsement
		await _sut.CommitAsync(endorsement.Id, cancellationTokenSource.Token);

		// 4. ensure the original driver record has been deleted
		(await _dbContext.Drivers.SingleOrDefaultAsync(x => x.DriverId == driver.DriverId, cancellationTokenSource.Token)).Should().BeNull();
		(await _dbContext.DriverEndorsements.SingleOrDefaultAsync(x => x.Id == endorsement.Id, cancellationTokenSource.Token)).Should().NotBeNull();

		// 5. ensure the endorsement record reflects it's already been approved
		// how? which column in the endorsement table gets updated when the endorsement is approved?
	}

	[TestMethod]
	public async Task RejectInsert()
	{
		using CancellationTokenSource cancellationTokenSource = new();

		var driverEndorsement = new DriverEndorsement
		                        {
			                        AgencyClientId   = 1,
			                        DriverDob        = new DateOnly(1975, 1, 1),
			                        DriverExcluded   = "N",
			                        DriverFirstName  = "First",
			                        DriverMiddleName = "Middle",
			                        DriverLastName   = "Last",
			                        DriverExperienceStartDate = DateTimeOffset.Now.AddMonths(-(5 * 12 + 6)).ToDateOnly(),
			                        DriverHireDate         = DateTimeOffset.Now.AddMonths(-(3 * 12)).ToDateOnly(),
			                        DriverLicenseClass     = "B",
			                        DriverLicenseNumber    = "1234",
			                        DriverLicenseState     = "CA",
			                        DriverType             = "anxious?",
			                        EndorsementStatusDate  = DateTimeOffset.Now.ToString(),
			                        EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly(),
			                        EndorsementStatus      = "insert",
			                        OperationType          = "insert"
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
		var                           driver = _dbContext.Drivers.GetRandomRecord();
		var                           endorsement = await _sut.UpdateAsync(driver, cancellationTokenSource.Token);
		await _sut.WithdrawAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated to show it's been withdrawn
		// - ensure the driver record in the driver table is NOT modified
	}

	[TestMethod]
	public async Task RejectDelete()
	{
		using CancellationTokenSource cancellationTokenSource = new();
		var                           driver = _dbContext.Drivers.GetRandomRecord();
		var                           endorsement = await _sut.DeleteAsync(driver.DriverId, cancellationTokenSource.Token);
		await _sut.WithdrawAsync(endorsement.Id, cancellationTokenSource.Token);

		// todo:
		// - ensure the endorsement record is updated to show it's been withdrawn
		// - ensure the driver record does NOT get deleted from the driver table
	}
}