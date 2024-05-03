using System.Linq.Expressions;
using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates.Factories
{
    public interface ISingleCriteriaPredicateFactory
    {
		BinaryExpression CreateComparisonExpression(FilteringInsurerCriterion criteria, ParameterExpression applicationParameter);

		Expression<Func<Application, bool>> Create(FilteringInsurerCriterion criteria, ParameterExpression applicationParameter);
	}
}