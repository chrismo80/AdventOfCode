using AdventOfCode;

namespace AdventOfCode2025;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var steps = input.Lines().Select(s => int.Parse(s.Replace("L", "-").Replace("R", "+")));

		yield return CountZeros(steps);
		yield return CountClicks(steps);
	}

	private static int CountZeros(IEnumerable<int> steps, int position = 50)
	{
		var count = 0;

		foreach (var step in steps)
			if (Step(ref position, step))
				count++;

		return count;
	}

	private static int CountClicks(IEnumerable<int> steps, int position = 50)
	{
		var count = 0;

		foreach (var step in steps)
		{
			var distance = step;

			while (distance-- > 0)
				if (Step(ref position, 1))
					count++;

			distance = step;

			while (distance++ < 0)
				if (Step(ref position, -1))
					count++;
		}

		return count;
	}

	private static bool Step(ref int position, int distance)
	{
		position += distance;
		position %= 100;

		return position == 0;
	}
}