using Extensions;

namespace AdventOfCode2024;

public static class Day6
{
	public static void Solve()
	{
		var map = Input.Load(2024, 6).ToMap();

		var current = new PathFinding.Grid<char>() { Map = map }
			.Find((value) => value != '.' && value != '#').First();

		var result1 = map.FindExit(current);
		var result2 = map.PlaceObstacles().Count(variant => variant.FindExit(current) == 0);

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static int FindExit(this char[][] map, (int X, int Y) current)
	{
		var direction = map[current.Y][current.X];
		var visited = new HashSet<(int, int)>() { current };

		var loopDetected = 100; // best guess
		var visitedTwice = 0;

		while (true)
		{
			var next = current.Next(direction);

			if (next.OutOfBounds(map))
				break;

			if (next.ObstacleDetected(map))
			{
				direction = direction.TurnRight();
				continue;
			}

			current = next;

			// try to detect endless loop by counting consecutive visits
			if (!visited.Add(current))
				visitedTwice++;
			else
				visitedTwice = 0;

			if (visitedTwice >= loopDetected)
				return 0;
		}

		return visited.Count;
	}

	private static bool OutOfBounds(this (int X, int Y) location, char[][] map) =>
		location.X < 0 || location.Y < 0 || location.X >= map.Length || location.Y >= map[0].Length;

	private static (int X, int Y) Next(this (int X, int Y) location, char direction) => direction switch
	{
		'^' => (X: location.X, Y: location.Y - 1),
		'v' => (X: location.X, Y: location.Y + 1),
		'<' => (X: location.X - 1, Y: location.Y),
		'>' => (X: location.X + 1, Y: location.Y),
		_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
	};

	private static bool ObstacleDetected(this (int X, int Y) location, char[][] map) =>
		map[location.Y][location.X] == '#' || map[location.Y][location.X] == 'O';

	private static char TurnRight(this char direction) => direction switch
	{
		'^' => '>',
		'>' => 'v',
		'v' => '<',
		'<' => '^',
		_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
	};

	private static IEnumerable<char[][]> PlaceObstacles(this char[][] map)
	{
		var width = map[0].Length;

		var flat = map.SelectMany(row => row).ToArray();

		for (var i = 0; i < flat.Length; i++)
		{
			if (flat[i] != '.')
				continue;

			flat[i] = 'O';

			yield return flat.Chunk(width).ToArray();

			flat[i] = '.';
		}
	}
}