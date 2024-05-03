using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TechQ.Core.Extensions;

public static class TypeExtensions
{
	[Pure]
	public static string GetName(this Type type)
	{
		if (type.IsNullable()) return GetNullableTypeName(type);

		if (type.IsFuncOrAction()) return FormatFuncOrAction(type);

		if (s_simpleTypeNames.Value.TryGetValue(type, out var name))
			return name;

		if (type.IsSimpleType()) return type.Name;

		return type.FormatWithCodeDomProvider(false);
	}

	[Pure]
	private static bool IsFuncOrAction(this Type type) => Regex.IsMatch(type.Name, @"^(((Func)|(Action))`[0-9]+)$");


	[Pure]
	private static string FormatFuncOrAction(Type type) => Regex.Replace(typeof(List<>).MakeGenericType(type).GetName(), @"^List\<(.+)\>$", "$1");

	[Pure]
	private static string FormatWithCodeDomProvider(this Type type, bool includeNamespaces)
	{
		var name = type.FormatWithCodeDomProvider().FixNullableNames();
		return includeNamespaces ? name : name.RemoveNamespaces();
	}

	private static string FixNullableNames(this string name) => Regex.Replace(name, @"(?:System\.)?Nullable\<([^>]*)\>", "$1?");
	private static string RemoveNamespaces(this string name) => Regex.Replace(name, @"(([a-zA-Z_][a-zA-Z0-9_]*)\.)+", "");

	private static string FormatWithCodeDomProvider(this Type type)
	{
		using var writer = new StringWriter();
		type.FormatWithCodeDomProvider(writer);
		return writer.GetStringBuilder().ToString();
	}

	private static void FormatWithCodeDomProvider(this Type type, TextWriter output)
	{
		var typeReferenceExpression = new CodeTypeReferenceExpression(new CodeTypeReference(type));
		var provider = CodeDomProvider.CreateProvider("C#");
		provider.GenerateCodeFromExpression(typeReferenceExpression, output, new());
	}


	[Pure]
	private static bool IsNullable(this Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);


	[Pure]
	private static string GetNullableTypeName(Type type)
	{
		var arg = type.GetGenericArguments()[0];
		return $"{arg.GetName()}?";
	}

	[Pure]
	public static bool IsSimpleType(this Type type) => type.Namespace == "System" || s_lazyLoadedSimpleTypeNames.Value.ContainsKey(type);


	private static readonly Lazy<IDictionary<Type, string>> s_simpleTypeNames = new(() => new Dictionary<Type, string>
	{
		{ typeof(string), "string" },
		{ typeof(char), "char" },
		{ typeof(bool), "bool" },
		{ typeof(void), "void" },

		{ typeof(TimeSpan), "TimeSpan" },
		{ typeof(DateOnly), "DateOnly" },
		{ typeof(DateTime), "DateTime" },
		{ typeof(DateTimeOffset), "DateTimeOffset" },

#pragma warning disable IDE0049

		{ typeof(byte), "byte" },
		{ typeof(sbyte), "sbyte" },

		{ typeof(Int16), "short" },
		{ typeof(Int32), "int" },
		{ typeof(Int64), "long" },

		{ typeof(UInt16), "ushort" },
		{ typeof(UInt32), "uint" },
		{ typeof(UInt64), "ulong" },

		{ typeof(Single), "float" },
		{ typeof(Double), "double" },
		{ typeof(Decimal), "decimal" }

#pragma warning restore IDE0049
	});

	private static readonly Lazy<IDictionary<Type, string>> s_lazyLoadedSimpleTypeNames = new(() => new Dictionary<Type, string>
	{
		{ typeof(string), "string" },
		{ typeof(char), "char" },
		{ typeof(bool), "bool" },
		{ typeof(void), "void" },

		{ typeof(TimeSpan), "TimeSpan" },
		{ typeof(DateOnly), "DateOnly" },
		{ typeof(DateTime), "DateTime" },
		{ typeof(DateTimeOffset), "DateTimeOffset" },

#pragma warning disable IDE0049

		{ typeof(byte), "byte" },
		{ typeof(sbyte), "sbyte" },

		{ typeof(Int16), "short" },
		{ typeof(Int32), "int" },
		{ typeof(Int64), "long" },

		{ typeof(UInt16), "ushort" },
		{ typeof(UInt32), "uint" },
		{ typeof(UInt64), "ulong" },

		{ typeof(Single), "float" },
		{ typeof(Double), "double" },
		{ typeof(Decimal), "decimal" }

#pragma warning restore IDE0049
	});

	[Pure]
	public static string GetName(this MethodInfo method)
	{
		var parameters = method.GetParameters()
			.Select(x => $"{x.ParameterType.GetName()} {x.Name}")
			.Join(", ");

		return $"{method.ReflectedType.GetName()}.{method.Name}({parameters})";
	}

	[Pure]
	public static bool IsEnumerable(this Type type) => type.GetInterfaces()
	                                                       .Where(x => x.IsGenericType)
	                                                       .Select(x => x.GetGenericTypeDefinition())
	                                                       .Any(x => x == typeof(IEnumerable<>));
}
