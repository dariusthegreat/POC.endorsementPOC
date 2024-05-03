using System.Diagnostics.Contracts;

namespace TechQ.Core.Extensions;

public static class DateTimeExtensions
{
	[Pure]
	public static DateOnly ToDateOnly(this DateTime dateTime) => DateOnly.FromDateTime(dateTime);

	[Pure]
	public static DateOnly ToDateOnly(this DateTimeOffset dateTime) => DateOnly.FromDateTime(dateTime.Date);

	[Pure]
	public static DateTime ToDateTime(this DateOnly dateOnly) => dateOnly.ToDateTime(TimeOnly.MinValue);

	[Pure]
	public static double GetYearsTo(this DateOnly a, DateOnly b) => a > b ? -GetYearsTo(b, a) : (b.Year - a.Year) / 365.25;


	[Pure]
	public static int GetYearsTo(this DateOnly a, DateTime b) => (int)a.GetYearsTo(b.ToDateOnly());
}
