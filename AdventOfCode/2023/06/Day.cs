using AdventOfCode;

namespace AdventOfCode2023;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		var times = input.Lines().First().Split(':').Last().ToArray<int>(" ");
		var distances = input.Lines().Last().Split(':').Last().ToArray<int>(" ");

		yield return 0;
	}
}