namespace AdventOfCode;

using System.Diagnostics;

public static class Problem
{
	public static void Solve()
	{
		long start = Stopwatch.GetTimestamp(), c = 0, runs = 1;

		while (c++ < runs)
			AdventOfCode2025.Day1.Solve();

		Console.WriteLine($"Duration: {Stopwatch.GetElapsedTime(start).Divide(runs).TotalMilliseconds:F1} ms");
	}
}