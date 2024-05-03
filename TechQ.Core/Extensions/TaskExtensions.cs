namespace TechQ.Core.Extensions;

public static class TaskExtensions
{
	public static Task WaitAll(this IEnumerable<Task> tasks) => Task.WhenAll(tasks);
	public static Task WaitAny(this IEnumerable<Task> tasks) => Task.WhenAny(tasks);

	public static Task<T[]> WaitAll<T>(this IEnumerable<Task<T>> tasks) => Task.WhenAll(tasks);
	public static Task<T> WaitAny<T>(this IEnumerable<Task<T>> tasks) => Task.WhenAny(tasks).Unwrap();
}
