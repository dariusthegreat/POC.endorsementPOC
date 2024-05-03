using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates;

public class SingleInsuranceCompanyPredicate : ISingleInsuranceCompanyPredicate
{
    private readonly InsuranceCompany _insuranceCompany;
    private readonly InsuranceDbContext _dbContext;

    private readonly Lazy<string[]> _lazyLoadedInsuranceCompanySupportedStates;
    private readonly Lazy<InsuranceCompanyFilteringCriteria[]> _lazyLoadedCriteria;

    public string InsureranceCompanyName => _insuranceCompany.InsuranceCoName;
    private string[] InsuranceCompanySupportedStates => _lazyLoadedInsuranceCompanySupportedStates.Value;
    private InsuranceCompanyFilteringCriteria[] FilteringCriteria => _lazyLoadedCriteria.Value;

    public SingleInsuranceCompanyPredicate(InsuranceDbContext dbContext, InsuranceCompany insuranceCompany)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _insuranceCompany = insuranceCompany ?? throw new ArgumentNullException(nameof(insuranceCompany));
        _lazyLoadedInsuranceCompanySupportedStates = new(GetSupportedStates);
        _lazyLoadedCriteria = new(LoadFilteringCriteria);
    }

    public SingleInsuranceCompanyFilteringResult Test(Application application) => new()
                                                                                   {
	                                                                                   Application = application,
	                                                                                   InsuranceCompany = _insuranceCompany,
	                                                                                   FilteringCriteriaResults = GetFilteringResults(application)
                                                                                   };

	private IDictionary<string, bool> GetFilteringResults(Application application)
    {
	    var applicationState = GetApplicationState(application);
	    var stateMatches     = InsuranceCompanySupportedStates.Contains(applicationState);

	    var stateResult = new { message = $"Agency Client Physical state should be supported by the insurance company. supported states: {string.Join(", ", InsuranceCompanySupportedStates)}", result = stateMatches };

		return FilteringCriteria.Select(x => new { message = x.ToString(), result = x.Matches(application)})
	                     .Reverse()
                         .Concat([stateResult])
	                     .Reverse()
	                     .ToDictionary(x => x.message, x=>x.result);
    }

	private string GetApplicationState(Application application) => _dbContext.LoadReference(application, x=>x.AgencyClient).PhysicalAddressState;

	private string[] GetSupportedStates() => _dbContext.LoadReferences(_insuranceCompany, x=>x.InsuranceCompanyStates)
	                                                   .Select(x => x.StateCode)
	                                                   .ToArray();

    private InsuranceCompanyFilteringCriteria[] LoadFilteringCriteria() => _dbContext.LoadReferences(_insuranceCompany, x => x.FilteringInsurerCriteria)
                                                                                     .Select(x => new InsuranceCompanyFilteringCriteria(x))
                                                                                     .ToArray();
}