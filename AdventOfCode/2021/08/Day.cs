using AdventOfCode;

namespace AdventOfCode2021;

public static class Day8
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(l => l.Split("|")
				.Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)));

		yield return data
			.Select(l => l.Last().Select(d => d.Length))
			.Sum(x => x.Count(d => d == 2 || d == 3 || d == 4 || d == 7));
	}
}