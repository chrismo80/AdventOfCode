using AdventOfCode;

namespace AdventOfCode2024;

public static class Day19
{
	private static System.Collections.Concurrent.ConcurrentDictionary<string, long> _cache = new();

	public static void Solve()
	{
		var input = Input.Load(2024, 19).ToArray<string>("\n\n");

		var towels = input.First().ToArray<string>(", ");
		var designs = input.Last().ToArray<string>("\n");

		var checks = designs.AsParallel()
			.Select(design => design.CountWays(towels.Where(t => design.Contains(t)).ToArray()));

		Console.WriteLine($"Part 1: {checks.Count(ways => ways > 0)}, Part 2: {checks.Sum()}");
	}

	private static long CountWays(this string remaining, string[] towels)
	{
		// remaining is empty, so design fully doable, add one more way
		if (remaining.Length == 0)
			return 1;

		// if already calculated, just return number of ways for remaining pattern from cache
		// if not yet calculated, do so and store number of ways for pattern in cache
		return _cache.GetOrAdd(remaining, (_) => towels.Where(remaining.StartsWith)
			.Sum(towel => remaining[towel.Length..].CountWays(towels)));
	}
}