using System.Reflection;

namespace AdventOfCode;

using System.Diagnostics;

public static class Problem
{
	public static void Solve((int Year, int Day) problem)
	{
		var input = Input.Load(problem.Year, problem.Day);

		var method = Assembly.GetExecutingAssembly()
			.GetType($"AdventOfCode{problem.Year}.Day{problem.Day}")
			.GetMethod("Solve");

		long start = Stopwatch.GetTimestamp(), i = 1;

		if (method.Invoke(null, [input]) is not IEnumerable<object> returned)
			return;

		var results = returned.ToList();

		Console.Write($"Year {problem.Year} Day {problem.Day:00}: {Stopwatch.GetElapsedTime(start).TotalMilliseconds:F1} ms".PadRight(40));

		foreach (var result in results)
			Console.Write($"Result {i++}: {result}".PadRight(30));

		Console.WriteLine();
	}
}