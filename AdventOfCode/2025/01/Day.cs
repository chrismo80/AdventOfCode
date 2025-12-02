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
		{
			position += step;
			position %= 100;

			if (position == 0)
				count++;
		}

		return count;
	}

	private static int CountClicks(IEnumerable<int> steps, int position = 50)
	{
		var count = 0;

		foreach (var step in steps)
		{
			var distance = step;

			while (distance-- > 0)
			{
				position++;
				position %= 100;

				if (position == 0)
					count++;
			}

			distance = step;

			while (distance++ < 0)
			{
				position--;
				position %= 100;

				if (position == 0)
					count++;
			}
		}

		return count;
	}
}