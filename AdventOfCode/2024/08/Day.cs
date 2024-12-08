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

		var antinodes = antennas.Select(type => type.FindAntinodes()).ToArray();

		var result1 = antinodes.SelectMany(x => x).Distinct()
			.Count(antinode => antinode.InBounds(map.Width, map.Height));

		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static IEnumerable<(int X, int Y)> FindAntinodes(this IEnumerable<(int X, int Y)> antennas)
	{
		foreach (var source in antennas)
		foreach (var target in antennas)
			if (source != target)
			{
				var zebras = (target.X + target.X - source.X, target.Y + target.Y - source.Y);
				yield return zebras;
			}
	}

	private static bool InBounds(this (int X, int Y) pos, int width, int height) =>
		pos.X >= 0 && pos.Y >= 0 && pos.X < width && pos.Y < height;
}