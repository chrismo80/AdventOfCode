using Extensions;

namespace AdventOfCode2024;

public static class Day1
{
	public static void Solve()
	{
		var input = Input.Load(2024, 1).ToNestedArray<int>("\n", " ");

		var left = input.Select(x => x.First()).Order().ToList();
		var right = input.Select(x => x.Last()).Order().ToList();

		var distances = left.Zip(right, (l, r) => Math.Abs(l - r));
		var similarities = left.Select(l => l * right.Count(r => r == l));

		Console.WriteLine($"Part 1: {distances.Sum()}, Part 2: {similarities.Sum()}");
	}
}