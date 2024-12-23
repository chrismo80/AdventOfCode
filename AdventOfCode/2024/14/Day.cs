using System.Text.RegularExpressions;
using AdventOfCode;
using Extensions;

namespace AdventOfCode2024;

public static class Day14
{
	public static void Solve()
	{
		var robots = Input.Load(2024, 14).Lines()
			.Match<int>(new Regex(@"p\=(-?\d+)\,(-?\d+)\ v\=(-?\d+)\,(-?\d+)"))
			.Select(groups => new Robot(groups))
			.ToArray();

		int width = 101, height = 103;

		var safetyFactors = new Dictionary<int, int>();

		for (var i = 0; i < 10_000; i++)
		{
			safetyFactors.Add(i, robots.GetSafetyFactor(width, height));

			if (safetyFactors.Values.Last() < 35_000_000)
				robots.Print(width, height);

			foreach (var robot in robots)
				robot.Move(width, height);
		}

		var result1 = safetyFactors[100];
		var result2 = Array.IndexOf(safetyFactors.Values.ToArray(), safetyFactors.Values.Min());

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static void Print(this Robot[] robots, int width, int height)
	{
		var text = new System.Text.StringBuilder();

		foreach (var (x, y) in from y in Enumerable.Range(0, height)
				from x in Enumerable.Range(0, width + 1)
				select (x, y))
			text.Append(x >= width ? '\n' :
				robots.Count(r => r.X == x && r.Y == y) == 0 ? '.' :
				robots.Count(r => r.X == x && r.Y == y).ToString());

		Console.WriteLine(text);
	}

	private static int GetSafetyFactor(this Robot[] robots, int width, int height) =>
		robots.GroupBy(r => r.GetQuadrant(width, height))
			.Where(g => g.Key > 0)
			.Select(g => g.Count())
			.Product();

	private class Robot(int[] input)
	{
		public int X { get; private set; } = input[0];
		public int Y { get; private set; } = input[1];

		private int VeloX { get; } = input[2];
		private int VeloY { get; } = input[3];

		public void Move(int width, int height)
		{
			X += VeloX + width;
			Y += VeloY + height;

			X %= width;
			Y %= height;
		}

		public int GetQuadrant(int width, int height)
		{
			if (X == (width - 1) / 2 || Y == (height - 1) / 2)
				return 0;

			return (X < width / 2 ? 0 : 1) + (Y < height / 2 ? 1 : 3);
		}
	}
}