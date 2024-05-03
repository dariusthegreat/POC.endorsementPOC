using System.Collections;
using TechQ.Entities.Models;
using TechQ.MgaFiltering.Predicates;

namespace TechQ.MgaFiltering;

public class SingleInsuranceCompanyFilteringResult : ICollection<KeyValuePair<string, bool>>
{
    private readonly Application _application;
    private readonly InsuranceCompany _insuranceCompany;

    public Application Application
    {
        get => _application;
        init
        {
            if (_application != null) throw new InvalidOperationException("Application already set. Cannot set application from both constructor & property initializer");
            _application = value ?? throw new ArgumentNullException(nameof(Application));
        }
    }

    public InsuranceCompany InsuranceCompany
    {
        get => _insuranceCompany;
        init
        {
            if (_insuranceCompany != null) throw new InvalidOperationException("InsuranceCompany already set. Cannot set insurance company from both constructor & property initializer");
            _insuranceCompany = value ?? throw new ArgumentNullException(nameof(InsuranceCompany));
        }
    }

    public IDictionary<string, bool> FilteringCriteriaResults
    {
        get => _results;
        init => _results = value ?? throw new ArgumentNullException(nameof(FilteringCriteriaResults));
    }

    private readonly IDictionary<string, bool> _results;

    // ReSharper disable once UnusedMember.Global
    public SingleInsuranceCompanyFilteringResult() { }

    public SingleInsuranceCompanyFilteringResult(Application application, InsuranceCompany insuranceCompany)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
        _insuranceCompany = insuranceCompany ?? throw new ArgumentNullException(nameof(insuranceCompany));
    }

    public IEnumerator<KeyValuePair<string, bool>> GetEnumerator() => _results.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_results).GetEnumerator();

    public bool this[InsuranceCompanyFilteringCriteria key]
    {
        get => this[key?.ToString()];
        set => this[key?.ToString()] = value;
    }

    public bool this[string key]
    {
        get => _results[key ?? throw new ArgumentNullException(nameof(key))];
        set => _results[key ?? throw new ArgumentNullException(nameof(key))] = value;
    }

    public int Count => _results.Count;
    public bool IsReadOnly => _results.IsReadOnly;

    public void Clear() => _results.Clear();
    public bool Contains(KeyValuePair<string, bool> item) => _results.Contains(item);
    public void CopyTo(KeyValuePair<string, bool>[] array, int arrayIndex) => _results.CopyTo(array, arrayIndex);
    public bool Remove(KeyValuePair<string, bool> item) => _results.Remove(item);


    public void Add(KeyValuePair<InsuranceCompanyFilteringCriteria, bool> item) => _results.Add(item.Key?.ToString() ?? throw new ArgumentNullException(nameof(item.Key)), item.Value);
    public void Add(InsuranceCompanyFilteringCriteria criterion, bool result) => Add(new KeyValuePair<InsuranceCompanyFilteringCriteria, bool>(criterion, result));
    public void Add((InsuranceCompanyFilteringCriteria criterion, bool result) tuple) => Add(tuple.criterion.ToString(), tuple.result);

    public void Add(KeyValuePair<string, bool> item) => _results.Add(item);
    public void Add(string criterionDescription, bool result) => Add(new KeyValuePair<string, bool>(criterionDescription, result));
    public void Add((string criterionDescription, bool result) tuple) => Add(TupleToKeyValuePair(tuple));

    private static KeyValuePair<string, bool> TupleToKeyValuePair((string criterionDescription, bool result) tuple)
    {
        var (criterionDescription, result) = tuple;
        return new(criterionDescription, result);
    }
}