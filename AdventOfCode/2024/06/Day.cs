using System.Diagnostics;
using Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace AdventOfCode2024;

public static class Day6
{
	public static void Solve()
	{
		var map = File.ReadAllLines("AdventOfCode/2024/06/Input.txt")
			.Select(row => row.ToArray()).ToArray();

		var current = new PathFinding.Grid<char>() { Map = map }
			.Find((value) => value != '.' && value != '#');

		var direction = map[current.Y][current.X];

		var visited = new HashSet<(int, int)>() { current };

		while (true)
		{
			var next = current.Next(direction);

			if (next.OutOfBounds(map))
				break;

			if (next.WallDetected(map))
			{
				direction = direction.TurnRight();
				continue;
			}

			current = next;
			visited.Add(current);
		}

		Console.WriteLine($"Part 1: {visited.Count}, Part 2: {0}");
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

	private static bool WallDetected(this (int X, int Y) location, char[][] map) =>
		map[location.Y][location.X] == '#';

	private static char TurnRight(this char direction) => direction switch
	{
		'^' => '>',
		'>' => 'v',
		'v' => '<',
		'<' => '^',
		_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
	};
}