using AdventOfCode;
using Extensions;

namespace AdventOfCode2025;

public static class Day8
{
	private record Point(long X, long Y, long Z);

	public static IEnumerable<object> Solve(string input)
	{
		var points = input.ToNestedArray<long>("\n", ",").Select(x => new Point(x[0], x[1], x[2])).ToArray();

		var distances = new List<(double Distance, Point Left, Point Right)>();

		for (var i = 0; i < points.Length; i++)
		for (var j = 0; j < points.Length; j++)
			if (i < j)
				distances.Add((Distance(points[i], points[j]), points[i], points[j]));

		distances = distances.Where(x => x.Distance > 0).OrderBy(x => x.Distance).ToList();

		var circuits = new List<HashSet<Point>>();

		foreach (var distance in distances.Take(10))
		{
			var circuit1 = circuits.FirstOrDefault(c => c.Contains(distance.Left));
			var circuit2 = circuits.FirstOrDefault(c => c.Contains(distance.Right));

			if (circuit1 is null && circuit2 is null)
			{
				circuits.Add(new HashSet<Point> { distance.Left, distance.Right });
				continue;
			}

			if (circuit1 is null || circuit2 is null)
			{
				circuit1?.Add(distance.Left);
				circuit1?.Add(distance.Right);
				circuit2?.Add(distance.Left);
				circuit2?.Add(distance.Right);
				continue;
			}

			if (circuit1 != circuit2)
			{
				circuit1.UnionWith(circuit2);
				circuits.Remove(circuit2);
			}
		}

		var counts = circuits.Select(c => c.Count).OrderByDescending(c => c);

		yield return counts.Take(3).Product();
	}

	private static double Distance(Point p1, Point p2)
		=> Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
}