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
	}
}