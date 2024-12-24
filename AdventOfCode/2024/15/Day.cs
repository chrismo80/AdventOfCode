using AdventOfCode;

namespace AdventOfCode2024;

public static class Day15
{
	public static void Solve()
	{
		var input = Input.Load(2024, 15).ToArray<string>("\n\n");

		var map = input[0].ToMap();
		var moves = input[1].Replace("\n", "");

		var boxes = map.Find('O').ToHashSet();
		var robot = map.Find('@').First();

		foreach (var move in moves)
		{
			var track = boxes.FindStack(robot, move).ToList();

			if (map[track.Last().Y][track.Last().X] == '#')
				continue;

			robot = track.First();
			boxes.Remove(robot);

			foreach (var pos in track.Skip(1))
				boxes.Add(pos);
		}

		var result1 = boxes.Select(b => b.Y * 100 + b.X).Sum();
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static IEnumerable<(int X, int Y)> FindStack(this HashSet<(int X, int Y)> boxes,
		(int X, int Y) pos, char move)
	{
		pos = pos.Next(move);

		while (boxes.Contains(pos))
		{
			yield return pos;
			pos = pos.Next(move);
		}

		yield return pos;
	}

	private static (int X, int Y) Next(this (int X, int Y) location, char direction) => direction switch
	{
		'^' => (X: location.X, Y: location.Y - 1),
		'v' => (X: location.X, Y: location.Y + 1),
		'<' => (X: location.X - 1, Y: location.Y),
		'>' => (X: location.X + 1, Y: location.Y),
		_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
	};
}