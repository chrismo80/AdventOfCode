using AdventOfCode;

namespace AdventOfCode2025;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var banks = input.Lines().Select(s => s.Select(c => c - '0').ToList());

		yield return banks.Sum(LargestJoltage);
	}

	private static int LargestJoltage(List<int> bank)
	{
		var max = bank.Max();
		var pos = bank.IndexOf(max);

		var left = bank.Take(pos).Select(x => int.Parse($"{x}{max}"));
		var right = bank.Skip(pos + 1).Select(x => int.Parse($"{max}{x}"));

		return Math.Max(left.Append(0).Max(), right.Append(0).Max());
	}
}