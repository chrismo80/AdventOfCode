using AdventOfCode;

namespace AdventOfCode2024;

public static class Day25
{
	public static void Solve()
	{
		var input = Input.Load(2024, 25).ToArray<string>("\n\n");

		var locks = input.Where(item => item[0] == '#').Select(GetHeights).ToArray();
		var keys = input.Where(item => item[0] == '.').Select(GetHeights).ToArray();

		var fits = keys.Sum(k => locks.Count(l => k.Fit(l)));

		Console.WriteLine($"Result: {fits}");
	}

	private static int[] GetHeights(this string input) => input.Lines().Skip(1).SkipLast(1)
		.Transpose().Select(row => row.Trim('.').Length).ToArray();

	private static bool Fit(this int[] keyHeights, int[] lockHeights, int space = 5) =>
		keyHeights.Zip(lockHeights, (k, l) => k + l).All(fit => fit <= space);
}