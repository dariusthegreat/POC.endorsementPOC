namespace TechQ.MgaFiltering.Predicates.Factories;

public interface ISingleInsuranceCompanyPredicateFactory
{
	ISingleInsuranceCompanyPredicate Create(string insuranceCompanyName);

	ISingleInsuranceCompanyPredicate Create(InsuranceCompany insuranceCompany);
}
