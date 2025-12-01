using AdventOfCode;

namespace AdventOfCode2020;

public static class Day17
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines()
			.Select(row => row.Select(c => c == '#').ToArray()).ToArray();

		var activeCubes = new HashSet<(int X, int Y, int Z, int W)>();

		for (var y = 0; y < lines.Length; y++)
		for (var x = 0; x < lines[0].Length; x++)
			if (lines[y][x])
				activeCubes.Add((x, y, 0, 0));

		var c = 6;

		while (c-- > 0)
		{
			int minX = activeCubes.Min(a => a.X), maxX = activeCubes.Max(a => a.X);
			int minY = activeCubes.Min(a => a.Y), maxY = activeCubes.Max(a => a.Y);
			int minZ = activeCubes.Min(a => a.Z), maxZ = activeCubes.Max(a => a.Z);
			int minW = activeCubes.Min(a => a.W), maxW = activeCubes.Max(a => a.W);

			var newActives = new HashSet<(int, int, int, int)>();

			foreach (var pos in from w in Enumerable.Range(minW - 1, maxW - minW + 3)
					from z in Enumerable.Range(minZ - 1, maxZ - minZ + 3)
					from y in Enumerable.Range(minY - 1, maxY - minY + 3)
					from x in Enumerable.Range(minX - 1, maxX - minX + 3)
					select (x, y, z, w))
			{
				var on = Neighbours(pos).Count(n => activeCubes.Contains(n));

				if ((activeCubes.Contains(pos) && on >= 2 && on <= 3) || (!activeCubes.Contains(pos) && on == 3))
					newActives.Add(pos);
			}

			activeCubes = newActives;
		}

		yield return 0;
		yield return activeCubes.Count;

		IEnumerable<(int, int, int, int)> Neighbours((int X, int Y, int Z, int W) pos)
		{
			return from w in Enumerable.Range(pos.W - 1, 3)
				from z in Enumerable.Range(pos.Z - 1, 3)
				from y in Enumerable.Range(pos.Y - 1, 3)
				from x in Enumerable.Range(pos.X - 1, 3)
				where !(pos.X == x && pos.Y == y && pos.Z == z && pos.W == w)
				select (x, y, z, w);
		}
	}
}