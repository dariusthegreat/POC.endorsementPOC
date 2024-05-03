using System.Diagnostics.Contracts;

namespace TechQ.Core.Extensions;

public static class  GeneralExtensionsAndHelpers
{
	[Pure]
	public static string ToOrdinal(this int cardinal) => $"{cardinal}{cardinal.GetOrdinalSuffix()}";

	
	[Pure]
	private static string GetOrdinalSuffix(this int value)
	{
		var r100 = value % 100;
		
		return (value % 10) switch
		{
			_ when (r100 >= 11 && r100 <= 13) => "th",
			1 => "st",
			2 => "nd",
			3 => "rd",
			_ => "th"
		};
	}
}
