using AdventOfCode;

namespace AdventOfCode2024;

public static class Day10
{
	public static void Solve()
	{
		var map = Input.Load(2024, 10).ToMap();

		var starts = map.Find('0');
		var ends = map.Find('9');

		var walkable = ((int X, int Y) pos) => pos.Neighbors(map[0].Length, map.Length)
			.Where(n => map[n.Y][n.X] - map[pos.Y][pos.X] == 1);

		var trails = starts.Select(start => ends.Select(end => walkable.FindTrails(start, end))).ToList();

		var result1 = trails.Sum(trail => trail.Count(t => t > 0));
		var result2 = trails.Sum(trail => trail.Sum());

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static int FindTrails<T>(this Func<T, IEnumerable<T>> walkableNeighbors, T start, T end)
	{
		var visited = 0;
		var active = new Queue<T>([start]);

		while (active.TryDequeue(out var current))
			foreach (var neighbor in walkableNeighbors(current))
			{
				if (neighbor!.Equals(end))
					visited++;

				active.Enqueue(neighbor);
			}

		return visited;
	}
}