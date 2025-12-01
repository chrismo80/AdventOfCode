using AdventOfCode;

namespace AdventOfCode2015;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var numbers = input.Lines()
			.Select(l => l.Split('x').Select(int.Parse).Order().ToArray());

		yield return numbers.Sum(b => 3 * b[0] * b[1] + 2 * b[1] * b[2] + 2 * b[2] * b[0]);
		yield return numbers.Sum(b => 2 * b[0] + 2 * b[1] + b[0] * b[1] * b[2]);
	}
}