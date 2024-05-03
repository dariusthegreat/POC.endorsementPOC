using TechQ.Entities;
using TechQ.Entities.Models;
using TechQ.MgaFiltering.Predicates;

namespace TechQ.MgaFiltering;

internal static class InternalExtensions
{
    /// <summary>
    /// Load the corresponding insurance companies (those registered with this MGA)
    /// </summary>
    /// <param name="mga"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
	public static Insurer LoadInsuranceCompanies(this Insurer mga, IInsuranceDbContext dbContext)
    {
        if (dbContext is InsuranceDbContext insuranceDbContext)
            insuranceDbContext.Entry(mga).Collection(x => x.InsuranceCos).Load();

        return mga;
    }

    /// <summary>
    /// Load the corresponding filtering criteria (records in the FilteringMgaCo table referencing this record through their insurance_co_id column)
    /// </summary>
    /// <param name="insuranceCompany"></param>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    /// <remarks>
    /// filtering_ic_criteria(insurance_co_id) -> filtering_mga_ic(insurance_co_id)
    /// </remarks>
    public static InsuranceCompany LoadFilteringCriteria(this InsuranceCompany insuranceCompany, IInsuranceDbContext dbContext)
    {
        if (dbContext is not InsuranceDbContext insuranceDbContext)
            return insuranceCompany;

        insuranceDbContext.Entry(insuranceCompany).Reference(x => x.FilteringInsurerCriteria).Load();
        insuranceDbContext.Entry(insuranceCompany).Reference(x => x.InsuranceCompanyStates).Load();

        return insuranceCompany;
    }


    public static MgaFilter CreateMgaFilter(this IEnumerable<ISingleInsuranceCompanyPredicate> insuranceCompanyPredicates, string mgaName) => new(mgaName, insuranceCompanyPredicates.ToArray());
}