using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates;

public class MgaFilter : ISingleMgaFilter
{
	private readonly ISingleInsuranceCompanyPredicate[] _insurerFilters;

	public string MgaName { get; }

	public MgaFilter(string mgaName, ISingleInsuranceCompanyPredicate[] insurerFilters)
    {
        if (string.IsNullOrWhiteSpace(mgaName)) throw new ArgumentException($"'{nameof(mgaName)}' cannot be null or empty or consist purely of whitespaces.", nameof(mgaName));
        MgaName = mgaName;
        _insurerFilters = insurerFilters ?? throw new ArgumentNullException(nameof(insurerFilters));
    }

    public SingleInsuranceCompanyFilteringResult[] Test(Application application) => _insurerFilters.Select(x => x.Test(application)).ToArray();
}
