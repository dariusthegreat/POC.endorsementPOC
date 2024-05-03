using System.Runtime.CompilerServices;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class DocumentNameAttribute : Attribute
{
	private readonly string _name;

	public string Name
	{
		get => _name;
		init => _name = ValidateName(value);
	}

	public Type GeneratorType { get; init; }

	public DocumentNameAttribute(string name) => Name = name;

	public DocumentNameAttribute() { }

	private string ValidateName(string value, [CallerArgumentExpression(nameof(value))] string parameterName = null)
	{
		if(_name != null) throw new InvalidOperationException($"Name already set through constructor to: [{_name}]. Cannot set it again to [{value}]");
		ArgumentNullException.ThrowIfNull(value, parameterName);
		if(value == "") throw new ArgumentException($"Invalid (empty) '{parameterName}'",                                 parameterName);
		if(string.IsNullOrWhiteSpace(value)) throw new ArgumentException($"Invalid (whitespaces only) '{parameterName}'", parameterName);

		return value;
	}
}