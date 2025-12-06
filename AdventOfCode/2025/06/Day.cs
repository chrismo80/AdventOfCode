using AdventOfCode;

namespace AdventOfCode2025;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		var parts = input.ToNestedArray<string>("\n", " ");

		var functions = new Dictionary<string, Func<long, long, long>>
		{
			["+"] = (x, y) => x + y,
			["*"] = (x, y) => x * y
		};

		var operations = parts.Last().Select(o => functions[o]).ToArray();

		var numbers = parts.SkipLast(1).Select(line => line.Select(long.Parse).ToArray()).ToArray();

		var results = numbers.First();

		foreach (var row in numbers.Skip(1))
			for (var i = 0; i < row.Length; i++)
				results[i] = operations[i](results[i], row[i]);

		yield return results.Sum();
	}
}