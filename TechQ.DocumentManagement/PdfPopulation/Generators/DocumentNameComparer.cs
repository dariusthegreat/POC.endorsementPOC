using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace TechQ.DocumentManagement.PdfPopulation.Generators;

public class DocumentNameComparer : IComparer<string>, IEqualityComparer<string>
{
    [Pure]
    public int Compare(string x, string y) => CompareNormalized(Normalize(x), Normalize(y));

	[Pure]
	public static int CompareNormalized(string x, string y) => new[] { x, y }.Contains("*") ? 0 : String.Compare(x, y, StringComparison.Ordinal);

	[Pure]
	// ReSharper disable once MemberCanBePrivate.Global
	public static string Normalize(string name) => NameRegex.Replace(name, "").Trim().ToLower().Replace("acord", "");

	public bool Equals(string x, string y) => Compare(x, y) == 0;

	public int GetHashCode([DisallowNull] string @string) => @string == "*" ? 0 : Normalize(@string).GetHashCode();

	private static Regex NameRegex => s_nameRegex.Value;
    private static readonly Lazy<Regex> s_nameRegex = new(() => new Regex(@"[^a-z0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase));
}