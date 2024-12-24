namespace AdventOfCode;

public static class MapExtensions
{
	public static char[][] ToMap(this string input) =>
		input.Lines().Select(row => row.ToArray()).ToArray();

	public static void Print(this char[][] map)
	{
		var s = new System.Text.StringBuilder();

		foreach (var row in map)
		{
			s.Append(row);
			s.AppendLine();
		}

		Console.WriteLine(s);
	}

	public static IEnumerable<(int X, int Y)> Find<T>(this T[][] map, T value)
	{
		for (var y = 0; y < map.Length; y++)
		for (var x = 0; x < map[0].Length; x++)
			if (value.Equals(map[y][x]))
				yield return (x, y);
	}

	public static IEnumerable<(int X, int Y)> Neighbors(this (int X, int Y) pos, int width, int height)
	{
		if (pos.X > 0) yield return pos with { X = pos.X - 1 };
		if (pos.Y > 0) yield return pos with { Y = pos.Y - 1 };
		if (pos.X < width - 1) yield return pos with { X = pos.X + 1 };
		if (pos.Y < height - 1) yield return pos with { Y = pos.Y + 1 };
	}

	public static IEnumerable<(int X, int Y)> Bfs(this Func<(int, int), IEnumerable<(int, int)>> walkableNeighbors,
		(int X, int Y) start, (int X, int Y) end)
	{
		var previous = new Dictionary<(int X, int Y), (int X, int Y)>();
		var visited = new HashSet<(int X, int Y)> { start };
		var active = new Queue<(int, int)>();

		active.Enqueue(start);

		while (active.Count > 0)
		{
			var current = active.Dequeue();

			if (current.Equals(end))
				while (!current.Equals(start))
				{
					yield return current;
					current = previous[current];
				}

			foreach (var neighbor in walkableNeighbors(current).Where(n => visited.Add(n)))
			{
				previous[neighbor] = current;
				active.Enqueue(neighbor);
			}
		}
	}
}