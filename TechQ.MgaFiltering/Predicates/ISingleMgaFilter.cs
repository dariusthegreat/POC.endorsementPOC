using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates;

public interface ISingleMgaFilter
{
    string                                  MgaName { get; }
    SingleInsuranceCompanyFilteringResult[] Test(Application application);
}
