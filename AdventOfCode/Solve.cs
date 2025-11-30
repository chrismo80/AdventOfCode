using System.Reflection;

namespace AdventOfCode;

using System.Diagnostics;

public static class Problem
{
	public static void Solve(int year = 2025, int day = 1)
	{
		var method = Assembly.GetExecutingAssembly()
			.GetType($"AdventOfCode{year}.Day{day}")
			.GetMethod("Solve");

		long start = Stopwatch.GetTimestamp(), i = 1;

		if (method.Invoke(null, [Input.Load(year, day)]) is not IEnumerable<object> results)
			return;

		foreach (var result in results)
			Console.Write($"Result {i++}: {result}\t\t\t");

		Console.WriteLine($"Duration: {Stopwatch.GetElapsedTime(start).TotalMilliseconds:F1} ms");
	}
}