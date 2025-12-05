using AdventOfCode;
using Extensions;

namespace AdventOfCode2025;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var banks = input.Lines().Select(s => s.Select(c => c - '0').ToList());

		yield return banks.Sum(bank => LargestJoltage(bank, 2));
		yield return banks.Sum(bank => LargestJoltage(bank, 12));
	}

	private static long LargestJoltage(List<int> bank, int length)
	{
		var joltage = new Dictionary<int, int>();

		var max = bank.Max();
		var pos = bank.IndexOf(max);

		joltage[pos] = max;

		var left = bank.Take(pos).Select(x => int.Parse($"{x}{max}"));
		var right = bank.Skip(pos + 1).Select(x => int.Parse($"{max}{x}"));

		return Math.Max(left.Append(0).Max(), right.Append(0).Max());
	}
}