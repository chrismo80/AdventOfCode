using System.Diagnostics;
using Extensions;

namespace AdventOfCode2024;

public static class Day5
{
	public static void Solve()
	{
		var input = Input.Load(2024, 5).ToArray<string>("\n\n");

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

	private static int[] Correct(this IEnumerable<int> update, int[][] rules)
	{
		var pages = update.ToArray();

		var violated = rules.FirstOrDefault(rule => !pages.Follows(rule));

		while (violated != null)
		{
			pages = pages.Swap(violated.First(), violated.Last());

			violated = rules.FirstOrDefault(rule => !pages.Follows(rule));
		}

		return pages;
	}

	private static int[] Swap(this int[] list, int a, int b)
	{
		list[Array.IndexOf(list, a)] = b;
		list[Array.IndexOf(list, b)] = a;

		return list;
	}
}