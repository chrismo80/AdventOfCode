using AdventOfCode;

namespace AdventOfCode2024;

public static class Day10
{
	public static void Solve()
	{
		var map = Helper.Load(2024, 10).ToMap();

		var trails = new List<List<(int, int)>>();

		foreach (var trailHead in map.FindStarts())
			trails.Add([trailHead]);

		var first = trails.First().Run(map);

		var result1 = 0;
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static List<(int X, int Y)> Run(this List<(int X, int Y)> trail, char[][] map) =>
		// for (var i = 0; i < count; i++)
		// {
		// 	var current = trails[i].Last();
		// 	var height = map[current.Y][current.X];
		//
		// 	if (height == '9')
		// 		continue;
		//
		// 	foreach (var neighbour in current.Neighbors(map).Where(p => map[p.Y][p.X] != height + 1))
		// 	{
		// 		var newTrail = trails[i].Append(neighbour).ToList();
		// 		trails.Add(newTrail);
		// 	}
		//
		// 	trails.Remove(trails[i]);
		// }
		//
		// return trails;
		default;

	private static IEnumerable<(int X, int Y)> FindStarts(this char[][] map)
	{
		foreach (var (x, y) in from y in Enumerable.Range(0, map.Length)
				from x in Enumerable.Range(0, map[0].Length)
				select (x, y))
			if (map[y][x] == '0')
				yield return (x, y);
	}

	private static IEnumerable<(int X, int Y)> Neighbors(this (int X, int Y) position, char[][] map)
	{
		if (position.Y > 0) yield return (position.X, position.Y - 1);
		if (position.X > 0) yield return (position.X - 1, position.Y);
		if (position.Y < map.Length - 1) yield return (position.X, position.Y + 1);
		if (position.X < map[0].Length - 1) yield return (position.X + 1, position.Y);
	}
}