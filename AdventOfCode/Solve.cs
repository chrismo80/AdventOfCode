namespace AdventOfCode;

using System.Diagnostics;

public static class Problem
{
	public static void Solve()
	{
		// required if run with Rider
		Directory.SetCurrentDirectory("../../../");

		long start = Stopwatch.GetTimestamp(), c = 0, runs = 1;

		while (c++ < runs)
		{
			AdventOfCode2024.Day1.Solve();
			AdventOfCode2024.Day2.Solve();
			AdventOfCode2024.Day3.Solve();
			AdventOfCode2024.Day4.Solve();
			AdventOfCode2024.Day5.Solve();
			AdventOfCode2024.Day6.Solve();
			AdventOfCode2024.Day7.Solve();
			AdventOfCode2024.Day8.Solve();
			AdventOfCode2024.Day9.Solve();
			AdventOfCode2024.Day10.Solve();
			AdventOfCode2024.Day11.Solve();
			AdventOfCode2024.Day13.Solve();
			AdventOfCode2024.Day14.Solve();
		}

		Console.WriteLine($"Duration: {Stopwatch.GetElapsedTime(start).Divide(runs).TotalMilliseconds:F1} ms");
	}
}