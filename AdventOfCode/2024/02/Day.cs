using AdventOfCode;

namespace AdventOfCode2024;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var reports = input.ToNestedArray<int>("\n", " ");

		var safeReports = reports.Where(l => IsSafe(Diffs(l)));

		var nearlySafeReports = reports.Except(safeReports)
			.Where(l => SubLevels(l).Select(Diffs).Any(IsSafe));

		yield return safeReports.Count();
		yield return safeReports.Count() + nearlySafeReports.Count();
	}

	private static IEnumerable<int> Diffs(IEnumerable<int> levels) =>
		levels.Skip(1).Zip(levels.SkipLast(1), (x, y) => x - y);

	private static bool IsSafe(IEnumerable<int> diffs) =>
		diffs.All(x => x >= 1 && x <= 3) || diffs.All(x => x <= -1 && x >= -3);

	private static IEnumerable<IEnumerable<int>> SubLevels(IEnumerable<int> levels) =>
		Enumerable.Range(0, levels.Count()).Select(i => levels.Where((_, index) => index != i));
}