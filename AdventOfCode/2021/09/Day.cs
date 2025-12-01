using AdventOfCode;

namespace AdventOfCode2021;

using Extensions;

public static class Day9
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines()
			.Select(l => l.Select(c => c - '0').ToArray()).ToArray();

		var sinks = from y in Enumerable.Range(0, lines.Length)
			from x in Enumerable.Range(0, lines[0].Length)
			where Neighbors(x, y).Select(n => lines[n.Y][n.X]).All(n => n > lines[y][x])
			select (X: x, Y: y);

		yield return sinks.Sum(sink => lines[sink.Y][sink.X]) + sinks.Count();
		yield return sinks.Select(BaisinSize).Order().TakeLast(3).Product();

		IEnumerable<(int X, int Y)> Neighbors(int x, int y)
		{
			if (y > 0) yield return (x, y - 1);
			if (x > 0) yield return (x - 1, y);
			if (y < lines.Length - 1) yield return (x, y + 1);
			if (x < lines[0].Length - 1) yield return (x + 1, y);
		}

		int BaisinSize((int X, int Y) start) // BreadthFirstSearch
		{
			var visited = new HashSet<(int, int)> { start };
			var active = new Queue<(int, int)>();

			active.Enqueue(start);

			while (active.TryDequeue(out (int X, int Y) current))
				foreach (var neighbor in Neighbors(current.X, current.Y)
							.Where(neighbor => lines[neighbor.Y][neighbor.X] < 9 && visited.Add(neighbor)))
					active.Enqueue(neighbor);

			return visited.Count;
		}
	}
}