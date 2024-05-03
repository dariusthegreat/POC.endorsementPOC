using TechQ.Core.Extensions;
using TechQ.Entities.Models;


namespace TechQ.Entities;

public static class Extensions
{
	public static int GetDriverYearsOfExperience(this Driver driver) => driver.DriverExperienceStartDate?.GetYearsTo(DateTime.Today) ?? 0;
}