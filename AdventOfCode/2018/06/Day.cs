using AdventOfCode;

namespace AdventOfCode2018;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(row => row.Split(", ").Select(int.Parse))
			.Select(point => (X: point.First(), Y: point.Last()))
			.ToList();

		var points = new Dictionary<int, HashSet<(int X, int Y)>>();
		var region = new HashSet<(int X, int Y)>();

		foreach (var point in from X in Enumerable.Range(data.Min(i => i.X), data.Max(i => i.X) - data.Min(i => i.X) + 1)
				from Y in Enumerable.Range(data.Min(i => i.Y), data.Max(i => i.Y) - data.Min(i => i.Y) + 1)
				select (X, Y))
		{
			if (GetTotalDistanceTo(point) < 10_000)
				region.Add(point);

			var closest = GetClosestIndexTo(point);

			if (closest < 0)
				continue;

			if (!points.ContainsKey(closest))
				points.Add(closest, new HashSet<(int X, int Y)>());

			points[closest].Add(point);
		}

		yield return points.Max(p => p.Value.Count);
		yield return region.Count;

		int GetClosestIndexTo((int X, int Y) point)
		{
			var closest = data.Select((p, i) => (Index: i, Distance: Math.Abs(p.Y - point.Y) + Math.Abs(p.X - point.X)))
				.OrderBy(d => d.Distance).ToArray();

			return closest[0].Distance == closest[1].Distance ? -1 : closest[0].Index;
		}

		int GetTotalDistanceTo((int X, int Y) point)
		{
			return data.Sum(p => Math.Abs(p.Y - point.Y) + Math.Abs(p.X - point.X));
		}
	}
}