using AdventOfCode;

namespace AdventOfCode2024;

public static class Day23
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input
			.ToNestedArray<string>("\n", "-")
			.Select(c => string.Join("-", c.Order()))
			.Distinct()
			.Order()
			.ToArray();

		var circles = new HashSet<string>();

		foreach (var c1 in data)
		foreach (var c2 in data)
		{
			var tuple = c1.Split("-").Concat(c2.Split("-")).Distinct().Order();

			if (tuple.Count() != 3)
				continue;

			var same = c1.Split("-").Intersect(c2.Split("-")).Single();

			var missing = string.Join("-", tuple.Except([same]).Order());

			if (data.Contains(missing))
				circles.Add(string.Join("-", tuple));
		}

		yield return circles.Count(circle => circle.Split("-").Any(c => c.StartsWith("t")));
	}
}