using AdventOfCode;
using Extensions;

namespace AdventOfCode2015;

public static class Day17
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<int>("/n");

		const int volume = 150;

		var combinations = data.Combinations().Where(c => c.Sum() == volume).ToArray();

		var minContainer = combinations.Min(c => c.Count());

		yield return combinations.Length;
		yield return combinations.Count(c => c.Count() == minContainer);
	}

	private static IEnumerable<IEnumerable<T>> Combinations2<T>(this IEnumerable<T> source) =>
		Enumerable.Range(1, 1 << source.Count()) // 2^input.Count
			.Select(bitMask => source.Where((_, index) => (bitMask & (1 << index)) != 0));

	private static IEnumerable<IEnumerable<T>> CombinationsBitMask<T>(this IEnumerable<T> source)
	{
		var count = (int)Math.Pow(2, source.Count());

		for (var combination = 1; combination < count; combination++)
			yield return ListOf(combination);

		IEnumerable<T> ListOf(int bitMask)
		{
			for (int i = source.Count() - 1, bit = count / 2; i >= 0; i--, bit /= 2)
				if ((bitMask & bit) != 0)
					yield return source.ElementAt(i);
		}
	}

	private static IEnumerable<IEnumerable<T>> CombinationsString<T>(this IEnumerable<T> source)
	{
		var count = Math.Pow(2, source.Count());

		for (var comb = 1; comb < count; comb++)
			yield return CombinationString(Convert.ToString(comb, 2).PadLeft(source.Count(), '0'));

		IEnumerable<T> CombinationString(string bitMask)
		{
			for (var pos = 0; pos < bitMask.Length; pos++)
				if (bitMask[pos] == '1')
					yield return source.ElementAt(pos);
		}
	}

	private static IEnumerable<IEnumerable<T>> WithoutRecursion<T>(this IEnumerable<T> source)
	{
		for (var counter = 0; counter < 1 << source.Count(); ++counter)
		{
			var combination = new List<T>();

			for (var i = 0; i < source.Count(); ++i)
				if ((counter & (1 << i)) == 0)
					combination.Add(source.ElementAt(i));

			yield return combination;
		}
	}

	private static IEnumerable<IEnumerable<T>> WithRecursion<T>(this IEnumerable<T> source)
	{
		for (var i = 0; i < 1 << source.Count(); i++)
			yield return Positions(i).Select(n => source.ElementAt(n));

		static IEnumerable<int> Positions(int i)
		{
			for (var n = 0; i > 0; n++)
			{
				if ((i & 1) == 1) yield return n;
				i /= 2;
			}
		}
	}
}