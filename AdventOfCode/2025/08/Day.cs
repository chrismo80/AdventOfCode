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

		foreach (var distance in distances)
		{
			var circuit = circuits.FirstOrDefault(c => c.Contains(distance.Left) || c.Contains(distance.Right));

			if (circuit is null)
			{
				circuits.Add(new HashSet<Point> { distance.Left, distance.Right });
			}
			else
			{
				circuit.Add(distance.Left);
				circuit.Add(distance.Right);
			}
		}

		yield return circuits.Select(c => c.Count).OrderByDescending(c => c).Take(3).Product();
	}

	private static double Distance(Point p1, Point p2)
		=> Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
}