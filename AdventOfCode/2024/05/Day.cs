using System.Diagnostics;
using Extensions;

namespace AdventOfCode2024;

public static class Day5
{
	public static void Solve()
	{
		var input = File.ReadAllText("AdventOfCode/2024/05/Input.txt")
			.Split("\n\n");

		var rules = input.First().Split('\n')
			.Select(x => x.Split('|').Select(int.Parse).ToArray())
			.ToArray();

		var updates = input.Last().Split('\n')
			.Select(row => row.Split(',').Select(int.Parse).ToArray())
			.ToArray();

		var correct = updates.Where(update => rules.All(update.Follows));
		var corrected = updates.Except(correct).Select(u => u.Correct(rules));

		Console.WriteLine($"Part 1: {correct.GetResult()}, Part 2: {corrected.GetResult()}");
	}

	private static int GetResult(this IEnumerable<int[]> updates) =>
		updates.Select(u => u.ElementAt(u.Length / 2)).Sum();

	private static bool Follows(this IEnumerable<int> update, int[] rule) =>
		!rule.All(update.Contains) || update.Intersect(rule).SequenceEqual(rule);

	private static int[] Correct(this IEnumerable<int> update, int[][] rules) =>
		update.Permutations().First(u => rules.All(u.Follows)).ToArray();
}