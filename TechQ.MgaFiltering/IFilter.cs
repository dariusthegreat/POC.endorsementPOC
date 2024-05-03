using TechQ.Entities.Models;

namespace TechQ.MgaFiltering
{
	public interface IFilter
	{
		/// <summary>
		/// Return list of MGAs that match the application criteria given an application ID.
		/// </summary>
		/// <param name="applicationId">Application ID</param>
		/// <returns>Matching MGA's</returns>
		Insurer[] GetMatches(int applicationId);

		/// <summary>
		/// Return list of MGAs that match the application criteria
		/// </summary>
		/// <param name="application"></param>
		/// <returns></returns>
		Insurer[] GetMatches(Application application);
	}
}
