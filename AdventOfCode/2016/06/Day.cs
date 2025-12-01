using AdventOfCode;

namespace AdventOfCode2016;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		var transposed = Enumerable.Range(0, lines[0].Length)
			.Select(i => lines.Select(word => word[i]));

		var mostCommon = transposed
			.Select(word => word.GroupBy(c => c).OrderBy(g => g.Count()).Select(g => g.Key).Last());

		var leastCommon = transposed
			.Select(word => word.GroupBy(c => c).OrderBy(g => g.Count()).Select(g => g.Key).First());

		yield return new string(mostCommon.ToArray());
		yield return new string(leastCommon.ToArray());
	}
}