using System.Linq.Expressions;
using System.Reflection;
using TechQ.Entities;
using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates.Factories;

public class SingleCriteriaPredicateFactory : ISingleCriteriaPredicateFactory
{
    private readonly IInsuranceDbContext _dbContext;

    public SingleCriteriaPredicateFactory(IInsuranceDbContext dbContext) => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));


	public BinaryExpression CreateComparisonExpression(FilteringInsurerCriterion criteria, ParameterExpression applicationParameter)
    {
		// relevant table columns:
		// 1. field     : use this field to create the selector to select the correct field from the application
		// 2. operation : use this field to create the comparison operator (<, <=, ==, >=, >, !=, etc)
		// 3. value     : constant value to compare to (make sure the units are parsed and replaced with standard units)
		var selectorExpression = CreateSelector(criteria, applicationParameter);
		var valueExpression = CreateConstant(criteria);
		return CreateOperator(criteria, selectorExpression, valueExpression);
	}

	public Expression<Func<Application, bool>> Create(FilteringInsurerCriterion criteria, ParameterExpression applicationParameter) => Expression.Lambda<Func<Application, bool>>(CreateComparisonExpression(criteria, applicationParameter), applicationParameter);


	private static Expression<Func<Application, object>> CreateSelector(FilteringInsurerCriterion criteria, ParameterExpression parameter)
	{
		//  questions:
		// 1. where do we find the value of "radius"?
		// 2. where do we find the commodities for a given application?
		// 3. how do we handle the states? (if the application contains states that are mentioned in the insurer states, fail the application. i.e., fail if application.states.except(insurer.states).Any())
		

		MethodInfo getAllDriversAgesMethod = null;

		return criteria.FieldName switch
		{
			"radius" => Expression.Lambda<Func<Application, object>>(Expression.Property(parameter, "Radius"), parameter),
			"commodity" => Expression.Lambda<Func<Application, object>>(Expression.Property(parameter, "Commo"), parameter),
			"driver age" => Expression.Lambda<Func<Application, object>>(Expression.Call(null, getAllDriversAgesMethod, parameter)),
			_ => throw new NotSupportedException($"field not supported: {criteria.FieldName}")
		};
	}

	private static BinaryExpression CreateOperator(FilteringInsurerCriterion criteria, Expression left, Expression right)
	{
		if(criteria.FieldName=="driver age")
		{
			throw new NotImplementedException();
		}


        return criteria.OperationTypeName switch
        {
            "less than"             => Expression.LessThan(left, right),
			"less than or equal"    => Expression.LessThanOrEqual(left, right),
            "equal"                 => Expression.Equal(left, right),
            "greater than or equal" => Expression.GreaterThanOrEqual(left, right),
			"greater than"          => Expression.GreaterThan(left, right),
            "not equal"             => Expression.NotEqual(left, right),
            "contains"              => throw new NotImplementedException(),
            "is one of"             => throw new NotImplementedException(),
            _                       => throw new NotSupportedException($"operation type not supported: {criteria.OperationTypeName}")
		};
	}

	private static ConstantExpression CreateConstant(FilteringInsurerCriterion criteria) => Expression.Constant(StandardizeUnits(criteria.FieldComparisonValue));

	/// <summary>
	/// todo: standardize units for distance (meters), duration (days), weight (Kg), etc
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	private static object StandardizeUnits(object value) => value;
}