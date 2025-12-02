using AdventOfCode;

namespace AdventOfCode2025;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var ranges = input.ToNestedArray<long>(",", "-");

		yield return ranges.Sum(range => GetInvalid(range.First(), range.Last()).Sum());
	}

	private static IEnumerable<long> GetInvalid(long start, long end)
	{
		for (var i = start; i <= end; i++)
		{
			var text = i.ToString();

			var left = text.Substring(0, text.Length / 2);
			var right = text.Substring(text.Length / 2);

			if (left == right)
				yield return i;
		}
	}
}