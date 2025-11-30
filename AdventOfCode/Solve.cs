using System.Reflection;

namespace AdventOfCode;

using System.Diagnostics;

public static class Problem
{
	public static void Solve(int year, int day)
	{
		if (!Input.Exists(year, day))
			return;

		var input = Input.Load(year, day);

		var method = Assembly.GetExecutingAssembly()
			.GetType($"AdventOfCode{year}.Day{day}")
			.GetMethod("Solve");

		long start = Stopwatch.GetTimestamp(), i = 1;

		if (method.Invoke(null, [input]) is not IEnumerable<object> results)
			return;

		Console.Write($"Year {year} Day {day:00}: {Stopwatch.GetElapsedTime(start).TotalMilliseconds:F1} ms".PadRight(40));

		foreach (var result in results)
			Console.Write($"Result {i++}: {result}".PadRight(30));

		Console.WriteLine();
	}
}