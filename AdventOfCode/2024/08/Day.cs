using AdventOfCode;

namespace AdventOfCode2024;

public static class Day8
{
	public static IEnumerable<object> Solve(string input)
	{
		var grid = new PathFinding.Grid<char> { Map = input.ToMap() };

		var types = grid.GetSymbols().Except(['.']);

		var antennas = types.Select(type => grid.Find((value) => value == type)).ToArray();

		yield return antennas.Count(grid.Width, grid.Height);
		yield return antennas.Count(grid.Width, grid.Height, grid.Width);
	}

	private static int Count(this IEnumerable<(int X, int Y)>[] antennas, int width, int height, int distance = 1) =>
		antennas
			.SelectMany(type => type.FindAntinodes(distance))
			.Distinct()
			.Count(antinode => antinode.IsInBounds(width, height));

	private static IEnumerable<(int X, int Y)> FindAntinodes(this IEnumerable<(int X, int Y)> antennas,
		int distance = 1)
	{
		foreach (var source in antennas)
		foreach (var target in antennas)
			if (source != target || distance > 1)
				foreach (var i in Enumerable.Range(1, distance))
					yield return (i * (target.X - source.X) + target.X, i * (target.Y - source.Y) + target.Y);
	}

	private static bool IsInBounds(this (int X, int Y) pos, int width, int height) =>
		pos.X >= 0 && pos.Y >= 0 && pos.X < width && pos.Y < height;
}