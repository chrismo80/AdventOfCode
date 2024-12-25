using AdventOfCode;

namespace AdventOfCode2024;

public static class Day12
{
	public static void Solve()
	{
		var map = Input.Load(2024, 12).ToMap();

		var regions = new HashSet<(int, int)[]>();

		var walkable = ((int X, int Y) pos) => pos.Neighbors(map[0].Length, map.Length)
			.Where(n => map[n.Y][n.X] == map[pos.Y][pos.X]);

		for (var y = 0; y < map.Length; y++)
		for (var x = 0; x < map[0].Length; x++)
			if (regions.All(r => !r.Contains((x, y))))
				regions.Add(walkable.FindRegion((x, y)));

		var result1 = regions.Sum(r => r.Count() * r.Perimeter());
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static int Perimeter(this IEnumerable<(int X, int Y)> region) =>
		region.Sum(plant => 4 - plant.Neighbors(int.MaxValue, int.MaxValue).Count(region.Contains));

	private static T[] FindRegion<T>(this Func<T, IEnumerable<T>> walkableNeighbors, T start)
	{
		var visited = new HashSet<T>([start]);
		var active = new Queue<T>([start]);

		while (active.TryDequeue(out var current))
			foreach (var neighbor in walkableNeighbors(current).Where(n => visited.Add(n)))
				active.Enqueue(neighbor);

		return visited.ToArray();
	}
}