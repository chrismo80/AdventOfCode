using AdventOfCode;
using NSubstitute.Extensions;

namespace AdventOfCode2024;

public static class Day25
{
	public static void Solve()
	{
		var input = Input.Load(2024, 25).ToArray<string>("\n\n");

		var locks = input.Where(item => item.First() == '#').Select(x => x.Parse()).ToArray();
		var keys = input.Where(item => item.Last() == '#').Select(x => x.Parse()).ToArray();

		var fits = locks.Sum(l => keys.Count(k => l.Fit(k)));

		Console.WriteLine($"Result: {fits}");
	}

	private static int[] Parse(this string input) =>
		input.Lines().Skip(1).SkipLast(1).Transpose().Select(row => row.Replace(".", "").Length).ToArray();

	private static bool Fit(this int[] lockHeights, int[] keyHeights, int space = 5) =>
		lockHeights.Zip(keyHeights, (l, k) => l + k).All(fit => fit <= space);
}