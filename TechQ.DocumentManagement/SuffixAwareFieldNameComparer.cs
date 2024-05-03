using System.Text.RegularExpressions;

namespace TechQ.DocumentManagement;

public class  SuffixAwareFieldNameComparer : IComparer<string>
{
    public int Compare(string x, string y) => FieldDetails.Compare(x, y);


	private static readonly Lazy<Regex> s_lazyLoadedRegex = new(() => new Regex(@"^(?<base>[^0-9]+)(?<suffix>((?<suffixNumber>[0-9]+)|(_(?<suffixLetter>[A-Z]))))?", RegexOptions.Compiled | RegexOptions.ExplicitCapture));
    private static Regex FieldNameRegex => s_lazyLoadedRegex.Value;

    private static FieldDetails Parse(string fieldName)
    {
        if (fieldName == null) throw new ArgumentNullException(nameof(fieldName));

		var match = FieldNameRegex.Match(fieldName);
        
        if (!match.Success) throw new ArgumentException($"Invalid field name: {fieldName}");

        var baseName = match.Groups["base"].Value;

        return (match.Groups["suffixNumber"].Success, match.Groups["suffixLetter"].Success) switch
        {
            (false, false) => new(baseName, 0),
            (true, false) => new(baseName, int.Parse(match.Groups["suffixNumber"].Value)),
            (false, true) => new(baseName, SuffixStringToIndex(match.Groups["suffixLetter"].Value)),
            (true, true) => throw new ArgumentException($"Invalid field name: '{fieldName}' names may have a numeric suffix or a letter, not both.", nameof(fieldName))
        };

        static int SuffixStringToIndex(string suffix) => suffix
            .Select((c, index) => new { c, index })
            .Sum(x => (x.c - 'A') * ((int)Math.Pow(26, suffix.Length - x.index)));
    }


	private record FieldDetails(string Name, int Suffix) : IComparable<FieldDetails>
	{
        public static implicit operator FieldDetails(string name) => Parse(name);

        public static int Compare(FieldDetails x, FieldDetails y) => x.CompareTo(y);

		public int CompareTo(FieldDetails other)
		{
            var nameComparisonResult = Name.CompareTo(other.Name);

            return nameComparisonResult != 0
                ? nameComparisonResult
				: Suffix.CompareTo(other.Suffix);
		}
	}
}