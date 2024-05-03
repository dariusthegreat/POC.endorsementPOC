using System.Diagnostics.Contracts;

namespace TechQ.Core.Extensions;

public static class CollectionExtensions
{
	[Pure]
	public static string Join<T>(this IEnumerable<T> sequence, string separator) => string.Join(separator, sequence);


	[Pure]
	public static IEnumerable<TInner> AntiJoin<TInner, TOuter, TKey>(this IEnumerable<TInner> inner, IEnumerable<TOuter> outer, Func<TInner, TKey> innerKeySelector, Func<TOuter, TKey> outerKeySelector) => inner.Except(from x in inner
																																																						  join y in outer on innerKeySelector(x) equals outerKeySelector(y)
																																																						  select x);

	public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
	{
		foreach (var item in sequence)
			action(item);
	}

	
	public static void Deconstruct<T>(this T[] array, out T item0, out T item1)
	{
		item0 = array[0];
		item1 = array[1];
	}
	public static void Deconstruct<T>(this T[] array, out T item0, out T item1, out T item2)
	{
		item0 = array[0];
		item1 = array[1];
		item2 = array[2];
	}
	public static void Deconstruct<T>(this T[] array, out T item0, out T item1, out T item2, out T item3)
	{
		item0 = array[0];
		item1 = array[1];
		item2 = array[2];
		item3 = array[3];
	}
	public static void Deconstruct<T>(this T[] array, out T item0, out T item1, out T item2, out T item3, out T item4)
	{
		item0 = array[0];
		item1 = array[1];
		item2 = array[2];
		item3 = array[3];
		item4 = array[4];
	}
	public static void Deconstruct<T>(this T[] array, out T item0, out T item1, out T item2, out T item3, out T item4, out T item5)
	{
		item0 = array[0];
		item1 = array[1];
		item2 = array[2];
		item3 = array[3];
		item4 = array[4];
		item5 = array[5];
	}
	public static void Deconstruct<T>(this T[] array, out T item0, out T item1, out T item2, out T item3, out T item4, out T item5, out T item6)
	{
		item0 = array[0];
		item1 = array[1];
		item2 = array[2];
		item3 = array[3];
		item4 = array[4];
		item5 = array[5];
		item6 = array[6];
	}
	public static void Deconstruct<T>(this T[] array, out T item0, out T item1, out T item2, out T item3, out T item4, out T item5, out T item6, out T item7)
	{
		item0 = array[0];
		item1 = array[1];
		item2 = array[2];
		item3 = array[3];
		item4 = array[4];
		item5 = array[5];
		item6 = array[6];
		item7 = array[7];
	}
}
