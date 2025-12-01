using AdventOfCode;

namespace AdventOfCode2024;

public static class Day19
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<string>("\n\n");

		var towels = data.First().ToArray<string>(", ");
		var designs = data.Last().ToArray<string>("\n");

		var checks = designs.AsParallel()
			.Select(design => design.CountWays(towels.Where(t => design.Contains(t)).ToArray()));

		yield return checks.Count(ways => ways > 0);
		yield return checks.Sum();
	}

	// memoization
	private static System.Collections.Concurrent.ConcurrentDictionary<string, long> _cache = new();

	// recursion
	private static long CountWays(this string pattern, string[] towels)
	{
		// design completely replaced by towels, return one new found new way
		if (pattern.Length == 0)
			return 1;

		// check if ways for pattern already calculated
		if (!_cache.ContainsKey(pattern)) // remove found towels from beginning of pattern and call again for rest
			_cache[pattern] = towels.Where(pattern.StartsWith).Sum(towel => pattern[towel.Length..].CountWays(towels));

		return _cache[pattern];
	}
}