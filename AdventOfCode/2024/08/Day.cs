namespace AdventOfCode2024;

public static class Day8
{
	public static void Solve()
	{
		var input = File.ReadAllLines("AdventOfCode/2024/08/Input.txt")
			.Select(row => row.ToArray()).ToArray();

		var map = new PathFinding.Grid<char>() { Map = input };

		var types = map.GetSymbols().Except(['.']);

		var antennas = types.Select(type => map.Find((value) => value == type)).ToArray();

		var result1 = antennas.Count(map.Width, map.Height);
		var result2 = antennas.Count(map.Width, map.Height, map.Width);

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
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