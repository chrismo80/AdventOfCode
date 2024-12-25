namespace AdventOfCode;

public static class MapExtensions
{
	public static char[][] ToMap(this string input) =>
		input.Lines().Select(row => row.ToArray()).ToArray();

	public static void Print(this char[][] map, List<(int, int)> path = null)
	{
		var s = new System.Text.StringBuilder();

		for (var y = 0; y < map.Length; y++)
		{
			for (var x = 0; x < map[0].Length; x++)
				if (path is not null && path.Contains((x, y)))
					s.Append('O');
				else
					s.Append(map[y][x]);

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

	public static IEnumerable<T> Bfs<T>(this Func<T, IEnumerable<T>> walkableNeighbors, T start, T end)
		where T : notnull
	{
		var visited = new Dictionary<T, T>();
		var active = new Queue<T>([start]);

		while (active.TryDequeue(out var current) && !current.Equals(end))
			foreach (var neighbor in walkableNeighbors(current).Where(n => !visited.ContainsKey(n)))
			{
				visited[neighbor] = current;
				active.Enqueue(neighbor);
			}

		return visited.Path(end, start).Reverse();
	}

	private static IEnumerable<T> Path<T>(this Dictionary<T, T> visited, T end, T start)
		where T : notnull
	{
		if (!visited.TryGetValue(end, out var pos))
			yield break;

		yield return end;

		while (!pos.Equals(start))
		{
			yield return pos;
			pos = visited[pos];
		}
	}
}