using Microsoft.EntityFrameworkCore;
using TechQ.Core.Extensions;
using TechQ.Entities.Models;

namespace TechQ.Endorsements;

public class DriverEndorsementManager(InsuranceDbContext dbContext) : IEndorsementManager<Driver,DriverEndorsement>
{
	private readonly InsuranceDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

	public async Task<DriverEndorsement> InsertAsync(DriverEndorsement endorsement, CancellationToken cancellationToken = default)
	{
		endorsement.OperationType = "insert";
		await _dbContext.DriverEndorsements.AddAsync(endorsement, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
		await _dbContext.Entry(endorsement).ReloadAsync(cancellationToken);
		return endorsement;
	}

	public async Task<DriverEndorsement> UpdateAsync(DriverEndorsement endorsement, CancellationToken cancellationToken = default)
	{
		endorsement.OperationType          = "update";
		endorsement.EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly();
		endorsement.EndorsementStatusDate  = $"{DateTimeOffset.Now:yyyy-MM-ddTHH:mm:ss.fffzzzz}";
		await _dbContext.DriverEndorsements.AddAsync(endorsement, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return endorsement;
	}

	public async Task<DriverEndorsement> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var driver      = await _dbContext.Drivers.SingleAsync(x => x.DriverId == id, cancellationToken);
		var endorsement = (DriverEndorsement) driver;
		endorsement.OperationType          = "delete";
		endorsement.EndorsementStatus      = "delete";
		endorsement.EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly();
		endorsement.EndorsementStatusDate  = $"{DateTimeOffset.Now:yyyy-MM-ddTHH:mm:ss.fffzzzz}";
		await _dbContext.DriverEndorsements.AddAsync(endorsement, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return endorsement;
	}

	public async Task<Driver> CommitAsync(long id, CancellationToken cancellationToken = default)
	{
		// what kind of endorsement is this? (as in, what is the operation type? insert/update/delete?)
		//		insert => insert a new record in the driver table
		//		update => update the existing record in the driver table
		//		delete => delete the existing record in the driver table

		var endorsement = await _dbContext.DriverEndorsements.SingleAsync(x => x.Id == id, cancellationToken);
		var commitAsync = GetCommitFunc(endorsement);

		await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			var driver = await commitAsync(endorsement, cancellationToken);
			await transaction.CommitAsync(cancellationToken);
			return driver;
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}

	private Func<DriverEndorsement, CancellationToken, Task<Driver>> GetCommitFunc(DriverEndorsement endorsement) => endorsement.OperationType.ToLower() switch
	                                                                                                                 {
		                                                                                                                 "insert" => CommitInsertAsync,
		                                                                                                                 "update" => CommitUpdateAsync,
		                                                                                                                 "delete" => CommitDeleteAsync,
		                                                                                                                 _        => throw new NotSupportedException($"operation not supported: {endorsement.OperationType}. Endorsement ID: {endorsement.Id}")
	                                                                                                                 };

	private async Task<Driver> CommitInsertAsync(DriverEndorsement endorsement, CancellationToken cancellationToken)
	{
		var inserted = await _dbContext.Drivers.AddAsync(endorsement, cancellationToken);
		endorsement.EndorsementStatus     = "inserted";
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:O}";
		await _dbContext.SaveChangesAsync(cancellationToken);
		return inserted.Entity;
	}

	private async Task<Driver> CommitUpdateAsync(DriverEndorsement endorsement, CancellationToken cancellationToken)
	{
		var driver = await _dbContext.Drivers.SingleAsync(x => x.DriverId == endorsement.DriverId, cancellationToken);
		driver.UpdateFrom(endorsement);
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:O}";
		endorsement.EndorsementStatus     = "updated";
		await _dbContext.SaveChangesAsync(cancellationToken);
		return driver;
	}

	private async Task<Driver> CommitDeleteAsync(DriverEndorsement endorsement, CancellationToken cancellationToken)
	{
		var driver = await _dbContext.Drivers.SingleAsync(x => x.DriverId == endorsement.DriverId, cancellationToken);
		_dbContext.Drivers.Remove(driver);
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:O}";
		endorsement.EndorsementStatus     = "deleted";
		await _dbContext.SaveChangesAsync(cancellationToken);
		return driver;
	}

	public Task<Driver> ApproveAsync(long id, CancellationToken cancellationToken = default) => CommitAsync(id, cancellationToken);

	public async Task WithdrawAsync(long id, CancellationToken cancellationToken = default)
	{
		var endorsement = await _dbContext.DriverEndorsements.SingleAsync(x => x.Id == id, cancellationToken);
		endorsement.OperationType         = "rejected";
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:yyyy-MM-ddTHH:mm:ss.fffzzzz}";
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}