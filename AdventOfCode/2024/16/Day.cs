using AdventOfCode;

namespace AdventOfCode2024;

public static class Day16
{
	public static IEnumerable<object> Solve(string input)
	{
		var map = input.ToMap();

		var start = map.Find('S').First();
		var end = map.Find('E').First();

		var walkable = ((int, int) pos) => pos.Neighbors(map[0].Length, map.Length)
			.Where(n => map[n.Y][n.X] != '#');

		yield return walkable.Search(start, end);
	}

	private static int Search(this Func<(int, int), IEnumerable<(int, int)>> walkableNeighbors,
		(int, int) start, (int, int) end)
	{
		var visited = new Dictionary<(int, int), int>();
		var active = new PriorityQueue<(int, int), (int Cost, char Dir)>([(start, (0, '>'))]);

		while (active.TryDequeue(out var current, out var prio))
			foreach (var neighbor in walkableNeighbors(current))
			{
				if (visited.ContainsKey(neighbor))
					continue;

				var cost = prio.Cost(current, neighbor);

				visited[neighbor] = cost.Value;

				active.Enqueue(neighbor, cost);
			}

		return visited[end];
	}

	private static (int Value, char Dir) Cost(this (int Cost, char Dir) prio, (int X, int Y) from, (int X, int Y) to)
	{
		var dir =
			to.X - from.X == 1 ? '>' :
			to.Y - from.Y == 1 ? 'v' :
			from.X - to.X == 1 ? '<' :
			from.Y - to.Y == 1 ? '^' :
			throw new Exception("invalid direction");

		return (prio.Cost + (prio.Dir == dir ? 1 : 1001), dir);
	}
}