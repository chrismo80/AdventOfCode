using AdventOfCode;
using System.Text.RegularExpressions;

namespace AdventOfCode2015;

public static class Day16
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(line => line.Split(new string[] { ": ", ", " }, StringSplitOptions.TrimEntries));

		var aunt = data.Single(items =>
			Greater(items, "cats", 7) &&
			Greater(items, "trees", 3) &&
			Fewer(items, "pomeranians", 3) &&
			Fewer(items, "goldfish", 5) &&
			Equals(items, "children", 3) &&
			Equals(items, "samoyeds", 2) &&
			Equals(items, "akitas", 0) &&
			Equals(items, "vizslas", 0) &&
			Equals(items, "cars", 2) &&
			Equals(items, "perfumes", 1))[0];

		yield return aunt;

		static bool Equals(string[] items, string name, int count)
		{
			return !items.Contains(name) || int.Parse(items[items.ToList().IndexOf(name) + 1]) == count;
		}

		static bool Greater(string[] items, string name, int count)
		{
			return !items.Contains(name) || int.Parse(items[items.ToList().IndexOf(name) + 1]) > count;
		}

		static bool Fewer(string[] items, string name, int count)
		{
			return !items.Contains(name) || int.Parse(items[items.ToList().IndexOf(name) + 1]) < count;
		}
	}
}