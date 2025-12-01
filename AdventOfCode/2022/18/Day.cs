using AdventOfCode;

namespace AdventOfCode2022;

public static class Day18
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines()
			.Select(l => l.Split(',').Select(int.Parse).ToArray())
			.Select(c => (X: c[0], Y: c[1], Z: c[2])).ToArray();

		var result1 = lines.Sum(c => 6 - BlockedSides(c, lines));
		var result2 = result1 - 0;

		yield return result1;
		yield return result2;

		static int BlockedSides((int X, int Y, int Z) c, (int, int, int)[] all)
		{
			return new List<(int, int, int)>
			{
				(c.X - 1, c.Y, c.Z), (c.X, c.Y - 1, c.Z), (c.X, c.Y, c.Z - 1),
				(c.X + 1, c.Y, c.Z), (c.X, c.Y + 1, c.Z), (c.X, c.Y, c.Z + 1)
			}.Count(all.Contains);
		}
	}
}