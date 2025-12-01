using AdventOfCode;

namespace AdventOfCode2018;

public static class Day18
{
	public static IEnumerable<object> Solve(string input)
	{
		var grid = input.Lines();

		Console.WriteLine(string.Join('\n', grid));

		var history = new HashSet<string> { string.Join('\n', grid) };

		int minutes = 0, cycleLength = 0, cycleStart = 0, duration = 1_000_000_000;

		while (minutes++ < duration)
		{
			grid = grid.Select((row, y) => new string(row.Select((acre, x) =>
				NewState(acre, Neighbors(x, y))).ToArray())).ToArray();

			if (cycleLength == 0 && !history.Add(string.Join('\n', grid)))
			{
				cycleStart = history.ToList().IndexOf(string.Join('\n', grid));
				cycleLength = minutes - cycleStart;
				minutes = cycleStart + (duration - cycleStart) / cycleLength * cycleLength;
			}
		}

		Console.WriteLine(string.Join('\n', grid));

		var result = grid.Sum(row => row.Count(acre => acre == '|')) *
			grid.Sum(row => row.Count(acre => acre == '#'));

		yield return result;

		char NewState(char acre, IEnumerable<char> neighbors)
		{
			return acre switch
			{
				'.' => neighbors.Count(n => n == '|') >= 3 ? '|' : acre,
				'|' => neighbors.Count(n => n == '#') >= 3 ? '#' : acre,
				'#' => neighbors.Any(n => n == '#') && neighbors.Any(n => n == '|') ? '#' : '.',
				_ => acre
			};
		}

		IEnumerable<char> Neighbors(int x, int y)
		{
			if (y > 0) yield return grid[y - 1][x];
			if (x > 0) yield return grid[y][x - 1];
			if (x > 0 && y > 0) yield return grid[y - 1][x - 1];
			if (x > 0 && y < grid!.Length - 1) yield return grid[y + 1][x - 1];
			if (y < grid.Length - 1) yield return grid[y + 1][x];
			if (x < grid[y].Length - 1) yield return grid[y][x + 1];
			if (x < grid[y].Length - 1 && y > 0) yield return grid[y - 1][x + 1];
			if (x < grid[y].Length - 1 && y < grid.Length - 1) yield return grid[y + 1][x + 1];
		}
	}
}