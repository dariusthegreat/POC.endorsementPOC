using Microsoft.EntityFrameworkCore;
using TechQ.Core.Extensions;
using TechQ.Entities.Models;

namespace TechQ.Endorsements;

public class VehicleEndorsementManager(InsuranceDbContext dbContext) : IEndorsementManager<Vehicle, VehicleEndorsement>
{
	private readonly InsuranceDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

	public async Task<VehicleEndorsement> InsertAsync(VehicleEndorsement endorsement, CancellationToken cancellationToken = default)
	{
		endorsement.OperationType          = "insert";
		endorsement.EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly();
		endorsement.EndorsementStatusDate  = $"{DateTimeOffset.Now:yyyy-MM-ddTHH:mm:ss.fffzzzz}";
		await _dbContext.VehicleEndorsements.AddAsync(endorsement, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
		await _dbContext.Entry(endorsement).ReloadAsync(cancellationToken);
		return endorsement;
	}

	public async Task<VehicleEndorsement> UpdateAsync(VehicleEndorsement endorsement, CancellationToken cancellationToken = default)
	{
		endorsement.OperationType          = "update";
		endorsement.EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly();
		endorsement.EndorsementStatusDate  = $"{DateTimeOffset.Now:yyyy-MM-ddTHH:mm:ss.fffzzzz}";
		await _dbContext.VehicleEndorsements.AddAsync(endorsement, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return endorsement;
	}

	public async Task<VehicleEndorsement> DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		var vehicle     = await _dbContext.Vehicles.SingleAsync(x => x.VehicleId == id, cancellationToken);
		var endorsement = (VehicleEndorsement) vehicle;
		endorsement.OperationType          = "delete";
		endorsement.EndorsementRequestDate = DateTimeOffset.Now.ToDateOnly();
		endorsement.EndorsementStatusDate  = $"{DateTimeOffset.Now:yyyy-MM-ddTHH:mm:ss.fffzzzz}";
		await _dbContext.VehicleEndorsements.AddAsync(endorsement, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return endorsement;
	}

	public async Task<Vehicle> CommitAsync(long id, CancellationToken cancellationToken = default)
	{
		var endorsement = await _dbContext.VehicleEndorsements.SingleAsync(x => x.Id == id, cancellationToken);
		var commitAsync = GetCommitFunc(endorsement);

		await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			var vehicle = await commitAsync(endorsement, cancellationToken);
			await transaction.CommitAsync(cancellationToken);
			return vehicle;
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}

	private Func<VehicleEndorsement, CancellationToken, Task<Vehicle>> GetCommitFunc(VehicleEndorsement endorsement) => endorsement.OperationType.ToLower() switch
	{
		"insert" => CommitInsertAsync,
		"update" => CommitUpdateAsync,
		"delete" => CommitDeleteAsync,
		_ => throw new NotSupportedException($"operation not supported: {endorsement.OperationType}. Vehicle Endorsement ID: {endorsement.Id}")
	};

	private async Task<Vehicle> CommitInsertAsync(VehicleEndorsement endorsement, CancellationToken cancellationToken)
	{
		var inserted = await _dbContext.Vehicles.AddAsync(endorsement, cancellationToken);
		endorsement.EndorsementStatus = "inserted";
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:O}";
		await _dbContext.SaveChangesAsync(cancellationToken);
		return inserted.Entity;
	}

	private async Task<Vehicle> CommitUpdateAsync(VehicleEndorsement endorsement, CancellationToken cancellationToken)
	{
		var vehicle = await _dbContext.Vehicles.SingleAsync(x => x.VehicleId == endorsement.VehicleId, cancellationToken);
		vehicle.UpdateFrom(endorsement);
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:O}";
		endorsement.EndorsementStatus = "updated";
		await _dbContext.SaveChangesAsync(cancellationToken);
		return vehicle;
	}

	private async Task<Vehicle> CommitDeleteAsync(VehicleEndorsement endorsement, CancellationToken cancellationToken)
	{
		var vehicle = await _dbContext.Vehicles.SingleAsync(x => x.VehicleId == endorsement.VehicleId, cancellationToken);
		_dbContext.Vehicles.Remove(vehicle);
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:O}";
		endorsement.EndorsementStatus = "deleted";
		await _dbContext.SaveChangesAsync(cancellationToken);
		return vehicle;
	}

	public Task<Vehicle> ApproveAsync(long id, CancellationToken cancellationToken = default) => CommitAsync(id, cancellationToken);

	public async Task WithdrawAsync(long id, CancellationToken cancellationToken = default)
	{
		var endorsement = await _dbContext.VehicleEndorsements.SingleAsync(x => x.Id == id, cancellationToken);
		endorsement.EndorsementStatus = "rejected";
		endorsement.EndorsementStatusDate = $"{DateTimeOffset.Now:yyyy-MM-ddTHH:mm:ss.fffzzzz}";
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}