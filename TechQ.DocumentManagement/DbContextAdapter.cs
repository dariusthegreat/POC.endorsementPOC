using System.Dynamic;
using System.Reflection;
using TechQ.Core.Extensions;

namespace TechQ.DocumentManagement;

public class DbContextAdapter : DynamicObject
{
	private readonly object                            _dataProvider;
    private readonly IDictionary<string, PropertyInfo> _propertyMap;

    public DbContextAdapter(object dataProvider)
    {
        _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

        _propertyMap =
        (
            from property in _dataProvider.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            where property.CanRead
            where property.PropertyType.IsEnumerable()
            select property
        )
        .ToDictionary(x => x.Name, x => x);
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        Console.Out.WriteLine($"DbContextAdapter.TryGetMember: {binder.Name}");

        if (_propertyMap.TryGetValue(binder.Name, out var property))
        {
            result = property.GetValue(_dataProvider);
            return true;
        }

        return base.TryGetMember(binder, out result);
    }
}