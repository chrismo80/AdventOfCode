namespace AdventOfCode2022;

public static class Day17
{
	public static IEnumerable<object> Solve(string input)
	{
		var stopped = Enumerable.Range(-3, 7).Select(s => (X: s, Y: 0)).ToHashSet();

		var w = 0;
		var last = stopped.Max(s => s.Y);
		var c = 0;

		for (var i = 0; i < 2022; i++) //(1000000000000 % 1745); i++)
		{
			var falling = NewShape(i % 5, stopped.Max(s => s.Y) + 4);

			while (!stopped.Intersect(falling).Any())
			{
				//PrintMap(stopped, falling);

				falling = input[w] == '<' ? falling.Left() : falling.Right();

				if (stopped.Intersect(falling).Any() || falling.Max(f => f.X) > 3 || falling.Min(f => f.X) < -3)
					falling = input[w] == '<' ? falling.Right() : falling.Left();

				w = (w + 1) % input.Length;

				//PrintMap(stopped, falling);

				falling = falling.Down();
			}

			stopped.UnionWith(falling.Up());

			if (w == 1399)
			{
				Console.WriteLine($"w: {w}, {i}: Shape {i % 5}, Height: {stopped.Max(s => s.Y) - last}, c: {c++}");
				last = stopped.Max(s => s.Y);
			}
		}

		yield return stopped.Max(s => s.Y);
		yield return stopped.Max(s => s.Y) + 1000000000000 / 1745 * 2778;
	}

	private static HashSet<(int X, int Y)> Left(this HashSet<(int X, int Y)> rock) => Move(rock, -1, 0);
	private static HashSet<(int X, int Y)> Right(this HashSet<(int X, int Y)> rock) => Move(rock, 1, 0);
	private static HashSet<(int X, int Y)> Down(this HashSet<(int X, int Y)> rock) => Move(rock, 0, -1);
	private static HashSet<(int X, int Y)> Up(this HashSet<(int X, int Y)> rock) => Move(rock, 0, 1);

	private static HashSet<(int X, int Y)> Move(HashSet<(int X, int Y)> rock, int x, int y) =>
		rock.Select(r => (r.X + x, r.Y + y)).ToHashSet();

	private static HashSet<(int X, int Y)> NewShape(int type = 0, int y = 0) =>
		type == 0 ? new HashSet<(int, int)> { (-1, y), (0, y), (1, y), (2, y) } :
		type == 1 ? new HashSet<(int, int)> { (0, y + 2), (-1, y + 1), (0, y + 1), (1, y + 1), (0, y) } :
		type == 2 ? new HashSet<(int, int)> { (-1, y), (0, y), (1, y), (1, y + 1), (1, y + 2) } :
		type == 3 ? new HashSet<(int, int)> { (-1, y), (-1, y + 1), (-1, y + 2), (-1, y + 3) } :
		type == 4 ? new HashSet<(int X, int Y)> { (-1, y), (-1, y + 1), (0, y), (0, y + 1) } :
		new HashSet<(int X, int Y)>();

	private static void PrintMap(HashSet<(int X, int Y)> stopped, HashSet<(int X, int Y)> falling)
	{
		Thread.Sleep(100);
		var output = new List<string>();

		for (var y = stopped.Max(w => w.Y) + 10; y >= stopped.Min(w => w.Y); y--)
		{
			var row = new List<char>();

			for (var x = stopped.Min(w => w.X); x <= stopped.Max(w => w.X); x++)
				row.Add(stopped.Contains((x, y)) ? '#' : falling.Contains((x, y)) ? '@' : '.');

			output.Add(new string(row.ToArray()));
		}

		File.WriteAllLines("AdventOfCode/2022/17/Output.txt", output);
	}
}