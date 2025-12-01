using AdventOfCode;

namespace AdventOfCode2024;

public static class Day15
{
	public static IEnumerable<object> Solve(string input)
	{
		var parts = input.ToArray<string>("\n\n");

		var map = parts[0].ToMap();

		var walls = map.Find('#').ToHashSet();
		var boxes = map.Find('O').ToHashSet();
		var robot = map.Find('@').First();

		foreach (var move in parts[1].Where(c => c != '\n'))
		{
			var stack = boxes.FindStack(robot, move).ToList();

			if (walls.Contains(stack.Last()))
				continue;

			robot = stack.First();
			boxes.Remove(robot);

			foreach (var pos in stack.Skip(1))
				boxes.Add(stack.Last());
		}

		yield return boxes.Select(b => b.Y * 100 + b.X).Sum();
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
		'^' => location with { Y = location.Y - 1 },
		'v' => location with { Y = location.Y + 1 },
		'<' => location with { X = location.X - 1 },
		'>' => location with { X = location.X + 1 },
		_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
	};
}