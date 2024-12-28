using AdventOfCode;

namespace AdventOfCode2024;

public static class Day19
{
	public static void Solve()
	{
		var input = Input.Load(2024, 19).ToArray<string>("\n\n");

		var towels = input.First().ToArray<string>(", ");
		var designs = input.Last().ToArray<string>("\n");

		var checks = designs.AsParallel()
			.Select(design => design.CountWays(towels.Where(t => design.Contains(t)).ToArray()));

		Console.WriteLine($"Part 1: {checks.Count(ways => ways > 0)}, Part 2: {checks.Sum()}");
	}

	// memoization
	private static System.Collections.Concurrent.ConcurrentDictionary<string, long> _cache = new();

	// recursion
	private static long CountWays(this string pattern, string[] towels)
	{
		// design completely replaced by towels, return one way
		if (pattern.Length == 0)
			return 1;

		// calculate for pattern by removing found towels from beginning of pattern and call again for rest
		if (!_cache.ContainsKey(pattern))
			_cache[pattern] = towels.Where(pattern.StartsWith).Sum(towel => pattern[towel.Length..].CountWays(towels));

		return _cache[pattern];
	}
}