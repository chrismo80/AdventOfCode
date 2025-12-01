using AdventOfCode;

namespace AdventOfCode2018;

public static class Day10
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(row => row.Split(',', '<', '>')
				.Where(_ => int.TryParse(_, out var v)).Select(int.Parse).ToArray())
			.Select(p => (startX: p[0], startY: p[1], deltaX: p[2], deltaY: p[3])).ToArray();

		var grid = data.Select(p => (X: p.startX, Y: p.startY)).ToArray();
		var velos = data.Select(p => (X: p.deltaX, Y: p.deltaY)).ToArray();

		var seconds = 0;

		while (grid.Max(p => p.Y) - grid.Min(p => p.Y) > 10)
		{
			for (var i = 0; i < velos.Length; i++)
			{
				grid[i].X += velos[i].X;
				grid[i].Y += velos[i].Y;
			}

			seconds++;
		}

		Print();

		yield return seconds;

		void Print()
		{
			foreach (var pos in from y in Enumerable.Range(grid.Min(p => p.Y), grid.Max(p => p.Y) - grid.Min(p => p.Y) + 1)
					from x in Enumerable.Range(grid.Min(p => p.X), grid.Max(p => p.X) - grid.Min(p => p.X) + 2)
					select (x, y))
				Console.Write(pos.x > grid.Max(p => p.X) ? '\n' : grid.Contains(pos) ? '#' : '.');
			Console.WriteLine();
		}
	}
}