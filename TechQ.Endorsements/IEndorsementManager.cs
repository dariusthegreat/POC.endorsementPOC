
namespace TechQ.Endorsements;

public interface IEndorsementManager<TEntity, TEndorsement>
	where TEntity : class
	where TEndorsement : class
{
	/// <summary>
	/// insert
	/// a new record is created in endorsement table, once approved, it will be copied to the regular table.  if not approved, it will be marked as Rejected.Operation_type is set to Insert.
	/// </summary>
	/// <returns>The ID of the endorsement record created in the endorsement table</returns>
	Task<TEndorsement> InsertAsync(TEndorsement endorsement, CancellationToken cancellationToken = default);


	/// <summary>
	/// update
	/// a new update record is created in endorsement table, once approved, it will be copied to the regular table.The old record is also copied to the endorsement table.  So the endorsement table will contain the before/after copies of the updated row.  if not approved, it will be marked as Rejected.Operation_type is set to Update.	/// </summary>
	Task<TEndorsement> UpdateAsync(TEndorsement endorsement, CancellationToken cancellationToken = default);

	/// <summary>
	/// delete
	/// a new delete record is created in endorsement table, once approved, the record gets deleted from the regular table.Operation_type is set to Delete.
	/// </summary>
	Task<TEndorsement> DeleteAsync(int id, CancellationToken cancellationToken = default);


	Task<TEntity> CommitAsync(long id, CancellationToken cancellationToken = default);

	Task<TEntity> ApproveAsync(long id, CancellationToken cancellationToken = default);
}