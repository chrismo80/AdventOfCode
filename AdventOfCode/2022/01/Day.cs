using AdventOfCode;
using Extensions;

namespace AdventOfCode2022;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<string>("\n")
			.SplitBy(row => string.IsNullOrEmpty(row))
			.Select(elf => elf.Select(cal => int.Parse(cal)).Sum())
			.OrderByDescending(elf => elf);

		yield return data.First();
		yield return data.Take(3).Sum();
	}
}