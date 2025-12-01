using AdventOfCode;

namespace AdventOfCode2020;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var results = Enumerable.Range(0, 5)
			.Select(slope => ((slope * 2 + 1) % 8, slope / 4 + 1))
			.Select(slope => CountTrees(slope, input.Lines()))
			.ToArray();

		yield return results[1];
		yield return results.Aggregate(1L, (x, y) => x * y);

		static int CountTrees((int X, int Y) slope, string[] forest)
		{
			var (X, Y, C) = (0, 0, 0);

			while (Y < forest.Length)
			{
				C += forest[Y][X] == '#' ? 1 : 0;
				Y += slope.Y;
				X += slope.X;
				X %= forest[0].Length;
			}

			return C;
		}
	}
}