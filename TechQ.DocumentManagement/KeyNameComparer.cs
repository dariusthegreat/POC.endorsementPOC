using System.Text.RegularExpressions;

namespace TechQ.DocumentManagement;

public class KeyNameComparer : IComparer<string>
{
	public int Compare(string x, string y) => ((ParsedKey)x).CompareTo((ParsedKey)y);

	record ParsedKey(string Name, int? Index) : IComparable<ParsedKey>
	{
        public ParsedKey((string name, int? index) tuple) : this(tuple.name, tuple.index) { }

		public ParsedKey(string key) : this(ParseKey(key)) { }

		private static (string name, int? index) ParseKey(string key)
        {
            var match = KeyRegex.Match(key);
			
			if(!match.Success)
				return (key, null);

			var name = match.Groups["name"].Value;
			var index = int.Parse(match.Groups["index"].Value);

			return (name, index);
        }

		public int CompareTo(ParsedKey other)
		{
			var namePortionComparisonResult = Name.CompareTo(other.Name);

			return (Index, other.Index) switch
			{
				_ when namePortionComparisonResult!=0 => namePortionComparisonResult,
				(null, null) => 0,
				(null, _) => -1,
				_ => Index.Value.CompareTo(other.Index)
			};
		}

		public static explicit operator ParsedKey(string key) => new ParsedKey(key);

		private static readonly Lazy<Regex> s_lazyLoadedRegex = new(() => new(@"^(?<name>[^0-9]+)(?<index>[0-9]+)$", RegexOptions.Compiled));
		private static Regex KeyRegex => s_lazyLoadedRegex.Value;
	}
}