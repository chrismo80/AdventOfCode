using AdventOfCode;

namespace AdventOfCode2022;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(line => (Opp: line[0] - 'A', You: line[2] - 'X'));

		yield return data.Sum(x => (4 + x.You - x.Opp) % 3 * 3 + x.You + 1);

		yield return data.Select(x => (x.Opp, You: (x.You + 2 + x.Opp) % 3))
			.Sum(x => (4 + x.You - x.Opp) % 3 * 3 + x.You + 1);
	}
}