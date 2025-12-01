using AdventOfCode;

namespace AdventOfCode2017;

public static class Day19
{
	public static IEnumerable<object> Solve(string input)
	{
		var map = input.ToMap();

		var (y, x) = (0, Enumerable.Range(0, map[0].Length).Single(i => map[0][i] != ' '));
		int steps = 0, face = 2; // 0 north, 1 east, 2 south, 3 west
		var letters = "";

		while (map[y][x] != ' ')
		{
			steps++;

			if (char.IsLetter(map[y][x]))
				letters += map[y][x];

			if (map[y][x] == '+' && face % 2 == 0)
			{
				if (x < 0 || map[y][x - 1] == ' ')
					face = 1;
				if (x == map[0].Length - 1 || map[y][x + 1] == ' ')
					face = 3;
			}
			else if (map[y][x] == '+' && face % 2 == 1)
			{
				if (y < 0 || map[y - 1][x] == ' ')
					face = 2;
				if (y == map.Length - 1 || map[y + 1][x] == ' ')
					face = 0;
			}

			x = face == 1 ? x + 1 : face == 3 ? x - 1 : x;
			y = face == 0 ? y - 1 : face == 2 ? y + 1 : y;
		}

		yield return letters;
		yield return steps;
	}
}