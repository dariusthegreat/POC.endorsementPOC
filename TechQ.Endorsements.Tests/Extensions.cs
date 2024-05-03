namespace TechQ.Endorsements.Tests;

public static class Extensions
{
	private static readonly Random s_random = new((int) DateTime.Now.Ticks);

	public static T GetRandomRecord<T>(this IEnumerable<T> collection)
	{
		var array = collection as T[] ?? collection.ToArray();
		var index = s_random.Next(array.Length);
		return array[index];
	}
}