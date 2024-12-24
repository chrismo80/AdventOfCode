using AdventOfCode;

namespace AdventOfCode2024;

public static class Day15
{
	public static void Solve()
	{
		var input = Input.Load(2024, 15).ToArray<string>("\n\n");

		var map = input[0].ToMap();
		var moves = input[1].Replace("\n", "");

		var grid = new PathFinding.Grid<char>() { Map = map };

		var boxes = grid.Find((x) => x == 'O').ToHashSet();
		var robot = grid.Find((x) => x == '@').First();

		var shift = map.FindTrack((4, 6), '^', boxes);

		foreach (var move in moves)
		{
			var track = map.FindTrack(robot, move, boxes).ToList();

			if (map[track.Last().Y][track.Last().X] == '#')
				continue;

			foreach (var pos in track.Skip(1))
				boxes.Add(pos);

			boxes.Remove(track.First());
			robot = track.First();
		}

		var result1 = boxes.Select(b => b.Y * 100 + b.X).Sum();
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static IEnumerable<(int X, int Y)> FindTrack(this char[][] map,
		(int X, int Y) pos, char move, HashSet<(int X, int Y)>? boxes)
	{
		pos = Next(pos, move);

		yield return pos;

		if (!pos.OutOfBounds(map) && boxes.Contains(pos))
			foreach (var next in map.FindTrack(pos, move, boxes))
				yield return next;
	}

	private static (int X, int Y) Next(this (int X, int Y) location, char direction) => direction switch
	{
		'^' => (X: location.X, Y: location.Y - 1),
		'v' => (X: location.X, Y: location.Y + 1),
		'<' => (X: location.X - 1, Y: location.Y),
		'>' => (X: location.X + 1, Y: location.Y),
		_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
	};

	private static bool OutOfBounds(this (int X, int Y) location, char[][] map) =>
		location.X < 0 || location.Y < 0 || location.X >= map.Length || location.Y >= map[0].Length;
}