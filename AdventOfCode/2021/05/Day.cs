using AdventOfCode;

namespace AdventOfCode2021;

using Extensions;

public static class Day5
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines()
			.Select(l => (
				X1: int.Parse(l.Split("->")[0].Trim().Split(",")[0]),
				Y1: int.Parse(l.Split("->")[0].Trim().Split(",")[1]),
				X2: int.Parse(l.Split("->")[1].Trim().Split(",")[0]),
				Y2: int.Parse(l.Split("->")[1].Trim().Split(",")[1])
			));

		yield return Enumerable.Range(0, 1000)
			.Select(x => Enumerable.Range(0, 1000).Select(y => lines
				.Where(p => p.X1 == p.X2 || p.Y1 == p.Y2)
				.Count(x, y)))
			.Sum(row => row.Count(p => p > 1));
	}

	private static int Count(this IEnumerable<(int X1, int Y1, int X2, int Y2)> data, int x, int y) =>
		data.Count(l => x.IsWithin(l.X1, l.X2) && y.IsWithin(l.Y1, l.Y2));
}