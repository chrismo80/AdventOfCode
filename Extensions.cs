using System.Runtime.CompilerServices;
using NSubstitute.ExceptionExtensions;

namespace Extensions;

public static class LinqExtensions
{
	public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
	{
		if (source.Count() == 1)
		{
			yield return source;
			yield break;
		}

		foreach (var variation in from e in source
				from p in Permutations(source.Where(s => !e.Equals(s)))
				select p.Prepend(e))
			yield return variation;
	}

	public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source) =>
		Enumerable.Range(1, (1 << source.Count()) - 1) // 1 .. (2^N)-1
			.Select(bitMask => source.Where((_, index) => (bitMask & (1 << index)) != 0));

	public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<T> source, int columnCount)
	{
		IEnumerable<IEnumerable<T>> result = [[]];

		for (var i = 0; i < columnCount; i++)
			result = result
				.SelectMany(existing => source, (existing, item) => existing.Append(item));

		return result;
	}

	public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		foreach (var item in source)
		{
			yield return item;

			if (predicate(item))
				yield break;
		}
	}

	public static IEnumerable<T> RepeatForever<T>(this IEnumerable<T> source)
	{
		while (true)
			foreach (var item in source)
				yield return item;
	}

	public static IEnumerable<int> AllIndexesOf(this string text, string match)
	{
		var index = text.IndexOf(match);

		while (index != -1)
		{
			yield return index;

			index = text.IndexOf(match, index + match.Length);
		}
	}

	public static IEnumerable<int> AllIndexesOf<T>(this IEnumerable<T> source, T match)
	{
		foreach (var item in source.Select((item, i) => (item, i)).Where(item => item.Equals(match)))
			yield return item.i;
	}

	public static IEnumerable<string> WhereMatches(this IEnumerable<string> source, string regex)
	{
		foreach (var item in source.Where(item => System.Text.RegularExpressions.Regex.IsMatch(item, regex)))
			yield return item;
	}

	public static IEnumerable<IEnumerable<T>> SplitBy<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		var chunk = new List<T>();

		foreach (var item in source)
		{
			if (predicate(item))
			{
				yield return chunk.ToList();
				chunk.Clear();
				continue;
			}

			chunk.Add(item);
		}

		yield return chunk;
	}

	public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach (var item in source)
			action(item);
	}

	public static int Product<T>(this IEnumerable<T> source, Func<T, int> selector) =>
		source.Select(item => selector(item)).Product();

	public static long Product<T>(this IEnumerable<T> source, Func<T, long> selector) =>
		source.Select(item => selector(item)).Product();

	public static double Product<T>(this IEnumerable<T> source, Func<T, double> selector) =>
		source.Select(item => selector(item)).Product();

	public static int Product(this IEnumerable<int> source) => source.Aggregate((x, y) => x * y);
	public static long Product(this IEnumerable<long> source) => source.Aggregate((x, y) => x * y);
	public static double Product(this IEnumerable<double> source) => source.Aggregate((x, y) => x * y);
}

public static class NumericsExtensions
{
	public static bool IsWithin(this int val, int min, int max) => (val - min) * (val - max) <= 0;
	public static bool IsWithin(this long val, long min, long max) => (val - min) * (val - max) <= 0;
	public static bool IsWithin(this double val, double min, double max) => (val - min) * (val - max) <= 0;
}

public static class StringExtensions
{
	public static T[] ToArray<T>(this string input, string delimiter) =>
		input.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => (T)Convert.ChangeType(item, typeof(T)))
			.ToArray();

	public static T[][] ToNestedArray<T>(this string input, string delim1, string delim2) =>
		input.Split(delim1, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => item.ToArray<T>(delim2))
			.ToArray();

	public static T[][][] ToNestedArray<T>(this string input, string d1, string d2, string d3) =>
		input.Split(d1, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => item.ToNestedArray<T>(d2, d3))
			.ToArray();

	public static char[][] ToMap(this string input) =>
		input.Split('\n').Select(row => row.ToArray()).ToArray();

	public static T Evaluate<T>(this string expression) => (T)Convert.ChangeType(new System.Data.DataTable()
		.Compute(expression.Replace("!=", "<>").Replace("==", "="), ""), typeof(T));
}

public static class Input
{
	public static string Load(int year, int day) =>
		File.ReadAllText(FileName(year, day));

	public static string[] LoadLines(int year, int day) =>
		File.ReadAllLines(FileName(year, day));

	private static string FileName(int year, int day) =>
		$"AdventOfCode/{year}/{day:00}/Input.txt";
}