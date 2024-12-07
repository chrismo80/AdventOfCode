using System.Diagnostics;
using Extensions;

namespace AdventOfCode2024;

public static class Day6
{
	public static void Solve()
	{
		var map = File.ReadAllLines("AdventOfCode/2024/06/Input.txt")
			.Select(row => row.ToArray()).ToArray();

		var grid = new PathFinding.Grid<char>()
		{
			Map = map,
			Walkable = (nX, nY, next, cX, cY, current, lX, lY, last) => next != '#' &&
			(
				(lX == cX && cX == nX) ||
				(lY == cY && cY == nY)
			)
		};

		var start = grid.Find((value) => value != '.' && value != '#');
		var last = (start.X, start.Y + 1);

		var path = grid.BreadthFirstSearch(start, (0, 0), last).ToList();

		Console.WriteLine(grid.Print());

		Console.WriteLine($"Part 1: {path.Count}, Part 2: {0}");
	}
}