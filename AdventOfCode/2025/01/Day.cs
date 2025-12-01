using AdventOfCode;

namespace AdventOfCode2025;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var steps = input.Lines().Select(s => int.Parse(s.Replace("L", "-").Replace("R", "+")));

		int position = 50, result1 = 0, click = 0;

		foreach (var step in steps)
		{
			position += step;

			while (position < 0)
			{
				position += 100;
				click++;
			}

			while (position > 99)
			{
				position -= 100;
				click++;
			}

			if (position == 0)
				result1++;
		}

		yield return result1;
		yield return click;
	}
}