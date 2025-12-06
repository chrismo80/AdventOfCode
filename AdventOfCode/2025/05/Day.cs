using AdventOfCode;

namespace AdventOfCode2025;

public static class Day5
{
	public static IEnumerable<object> Solve(string input)
	{
		var parts = input.ToArray<string>("\n\n");

		var ranges = parts[0].ToNestedArray<long>("\n", "-");

		var ids = parts[1].ToArray<long>("\n");

		yield return ids.Count(id => ranges.Any(range => id >= range.First() && id <= range.Last()));

		ranges = ranges.OrderBy(r => r[0]).ToArray();

		var mergedRanges = new List<long[]> { ranges.First() };

		foreach (var range in ranges.Skip(1))
		{
			var last = mergedRanges.Last();

			if (range[0] > last[1])
				mergedRanges.Add(range);
			else
				last[1] = Math.Max(last[1], range[1]);
		}

		yield return mergedRanges.Sum(r => r[1] - r[0] + 1);
	}
}