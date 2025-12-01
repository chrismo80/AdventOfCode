namespace AdventOfCode2016;

public static class Day19
{
	public static IEnumerable<object> Solve(string input)
	{
		var count = int.Parse(input);

		int player1 = 1, diff = 1, player2 = 1, count2 = count;

		while (count > 1)
		{
			diff *= 2;

			if (count % 2 != 0)
				player1 += diff;

			count /= 2;
		}

		yield return player1;
	}
}