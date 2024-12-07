using System.Diagnostics;
using Extensions;

namespace AdventOfCode2024;

public static class Day6
{
	public static void Solve()
	{
		var map = File.ReadAllLines("AdventOfCode/2024/06/Input.txt").Select(row => row.ToArray()).ToArray();

		var grid = new PathFinding.Grid<char>() { Map = map, Walkable = (_, _, next, _) => next != '#' };

		var start = grid.Find((value) => value != '.' && value != '#');

		Console.WriteLine(grid.Print());

		Console.WriteLine($"Part 1: {0}, Part 2: {0}");
	}
}