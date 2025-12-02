using AdventOfCode;

namespace AdventOfCode2025;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var steps = input.Lines().Select(s => int.Parse(s.Replace("L", "-").Replace("R", "+")));

		yield return CountZeros(steps);
	}

	private static int CountZeros(IEnumerable<int> steps)
	{
		int position = 50, count = 0;

		foreach (var step in steps)
		{
			position += step;
			position %= 100;

			if (position == 0)
				count++;
		}

		return count;
	}
}