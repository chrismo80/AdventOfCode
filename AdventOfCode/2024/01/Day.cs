using AdventOfCode;

namespace AdventOfCode2024;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToNestedArray<int>("\n", " ");

		var left = data.Select(x => x.First()).Order().ToList();
		var right = data.Select(x => x.Last()).Order().ToList();

		yield return left.Zip(right, (l, r) => Math.Abs(l - r)).Sum();
		yield return left.Sum(l => l * right.Count(r => r == l));
	}
}