using TechQ.Entities;
using TechQ.Entities.Models;


// ReSharper disable MemberCanBePrivate.Global


namespace TechQ.MgaFiltering.Predicates.Factories;

public class MgaFilterFactory
{
    private readonly IInsuranceDbContext                     _dbContext;
    private readonly ISingleInsuranceCompanyPredicateFactory _singleInsurerPredicateFactory;

    // ReSharper disable once ConvertToPrimaryConstructor
    public MgaFilterFactory(IInsuranceDbContext dbContext, ISingleInsuranceCompanyPredicateFactory singleInsurerPredicateFactory= null)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _singleInsurerPredicateFactory = singleInsurerPredicateFactory ?? new SingleInsuranceCompanyPredicateFactory(dbContext);
    }

    public ISingleMgaFilter Create(string mgaName) => _dbContext.Insurers                                                                    
                                                                   .Where(x => x.InsurerType == "M")         // include only MGA records from the insurers table
                                                                   .Single(x => x.InsurerCoName == mgaName)  // find the MGA record with the given name
                                                                   .LoadInsuranceCompanies(_dbContext)              // load its corresponding insurance company records
                                                                   .InsuranceCos!                                   // this InsuranceCos property is not null only because of the call to LoadInsuranceCompanies() in the previous line
                                                                   .Select(CreateSingleInsuranceCompanyPredicate)   // map each insurance company record to a single insurance company predicate
                                                                   .CreateMgaFilter(mgaName);                       // create a single MGA filter using the MGA name & the resulting predicates obtained in the previous line

	public ISingleMgaFilter Create(Insurer mga) => Create(mga.InsurerCoName);

	private ISingleInsuranceCompanyPredicate CreateSingleInsuranceCompanyPredicate(InsuranceCompany insuranceCompany) => _singleInsurerPredicateFactory.Create(insuranceCompany);

	public ISingleMgaFilter[] CreateAll() => _dbContext.Insurers
	                                                   .Where(x => x.InsurerType == "M")
	                                                   .Select(Create)
	                                                   .ToArray();

	public static ISingleMgaFilter[] CreateAll(IInsuranceDbContext dbContext) => new MgaFilterFactory(dbContext).CreateAll();
}

