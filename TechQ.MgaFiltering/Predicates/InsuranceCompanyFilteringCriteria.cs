using TechQ.Entities.Models;

namespace TechQ.MgaFiltering.Predicates;

public class InsuranceCompanyFilteringCriteria
{
    private readonly FilteringInsurerCriterion _criterion;
    private readonly Lazy<string> _lazyLoadedDescription;

    public string Description => _lazyLoadedDescription.Value;

    public InsuranceCompanyFilteringCriteria(FilteringInsurerCriterion criterion)
    {
        _criterion = criterion;
        _lazyLoadedDescription = new(CreateDescription);
    }

    private string CreateDescription() => $"{_criterion.FieldName} {_criterion.OperationTypeName} {_criterion.FieldComparisonValue}";

    public bool Matches(Application application) => _criterion.FieldName switch
    {
        "radius" => DoesRadiusMatch(application),
        "commodity" => DoesCommodityMatch(application),
        "driver age" => DoesDriverAgeMatch(application),
        "driver experience" => DoesDriverExperienceMatch(application),
        "class A driver license" => DoesDriverClassADriverLicenseMatch(application),
        "dump" => DoesDumpRadiusMatch(application),
        _ => throw new NotImplementedException()
    };

    private bool DoesRadiusMatch(Application application)
    {
        throw new NotImplementedException();
    }

    private bool DoesCommodityMatch(Application application)
    {
        throw new NotImplementedException();
    }

    private bool DoesDriverAgeMatch(Application application)
    {
        throw new NotImplementedException();
    }

    private bool DoesDriverExperienceMatch(Application application)
    {
        throw new NotImplementedException();
    }

    private bool DoesDriverClassADriverLicenseMatch(Application application)
    {
        throw new NotImplementedException();
    }

    private bool DoesDumpRadiusMatch(Application application)
    {
        throw new NotImplementedException();
    }


}