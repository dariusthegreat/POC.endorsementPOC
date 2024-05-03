using TechQ.Entities;
using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates.Factories;

public class SingleInsuranceCompanyPredicateFactory : ISingleInsuranceCompanyPredicateFactory
{
    private readonly IInsuranceDbContext _dbContext;
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public SingleInsuranceCompanyPredicateFactory(IInsuranceDbContext dbContext) => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public ISingleInsuranceCompanyPredicate Create(string insuranceCompanyName) => Create(GetInsuranceCompanyByName(insuranceCompanyName));

	public ISingleInsuranceCompanyPredicate Create(InsuranceCompany insuranceCompany) => new SingleInsuranceCompanyPredicate(_dbContext as InsuranceDbContext, insuranceCompany);

	private InsuranceCompany GetInsuranceCompanyByName(string insuranceCompanyName) => (_dbContext is InsuranceDbContext dbContext) ? dbContext.FilteringMgaIcs.Single(x => x.InsuranceCoName == insuranceCompanyName) : null;
}
