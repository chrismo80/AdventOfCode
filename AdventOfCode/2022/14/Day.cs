using AdventOfCode;

namespace AdventOfCode2022;

public static class Day14
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Append("300,173 -> 700,173")
			.Select(line => line.Split(" -> ")
				.Select(point => point.Split(',').Select(int.Parse).ToArray())
				.Select(point => (X: point[0], Y: point[1])).ToArray())
			.ToArray();

		var walls = new HashSet<(int X, int Y)>();

		foreach (var row in data)
			for (var i = 1; i < row.Length; i++)
			{
				var wall = from X in
						Enumerable.Range(Math.Min(row[i].X, row[i - 1].X), Math.Abs(row[i].X - row[i - 1].X) + 1)
					from Y in
						Enumerable.Range(Math.Min(row[i].Y, row[i - 1].Y), Math.Abs(row[i].Y - row[i - 1].Y) + 1)
					select (X, Y);

				foreach (var brick in wall)
					walls.Add(brick);
			}

		var sand = new HashSet<(int X, int Y)>();
		var unit = (X: 500, Y: 0);

		while (!sand.Contains(unit)) // && walls.Max(w => w.Y) > unit.Y) // Part 1
		{
			//sand.Add(unit); PrintMap(walls, sand); sand.Remove(unit);

			unit.Y++;
			if (!sand.Contains(unit) && !walls.Contains(unit)) continue;

			unit.X--;
			if (!sand.Contains(unit) && !walls.Contains(unit)) continue;

			unit.X += 2;
			if (!sand.Contains(unit) && !walls.Contains(unit)) continue;

			sand.Add((--unit.X, --unit.Y));
			unit = (500, 0);
		}

		yield return sand.Count;

		PrintMap(walls, sand);
	}

	private static void PrintMap(HashSet<(int X, int Y)> walls, HashSet<(int X, int Y)> sand)
	{
		var output = new List<string>();

		for (var y = 0; y <= walls.Max(w => w.Y); y++)
		{
			var row = new List<char>();

			for (var x = walls.Min(w => w.X); x <= walls.Max(w => w.X); x++)
				row.Add(walls.Contains((x, y)) ? '#' : sand.Contains((x, y)) ? 'o' : '.');

			output.Add(new string(row.ToArray()));
		}

		Output.Save(2022, 14, string.Join(Environment.NewLine, output));
	}
}