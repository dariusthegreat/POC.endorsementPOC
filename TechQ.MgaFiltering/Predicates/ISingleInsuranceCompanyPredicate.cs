using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates;

public interface ISingleInsuranceCompanyPredicate
{
    string                                InsureranceCompanyName        { get; }
    SingleInsuranceCompanyFilteringResult Test(Application application);
}


