using AdventOfCode;

namespace AdventOfCode2024;

public static class Day23
{
	public static void Solve()
	{
		var input = Input.Load(2024, 23)
			.ToNestedArray<string>("\n", "-")
			.Select(c => string.Join("-", c.Order()))
			.Distinct()
			.Order()
			.ToArray();

		var cirles = new HashSet<string>();

		foreach (var c1 in input)
		foreach (var c2 in input)
		{
			var tuple = c1.Split("-").Concat(c2.Split("-")).Distinct().Order();

			if (tuple.Count() != 3)
				continue;

			var same = c1.Split("-").Intersect(c2.Split("-")).Single();

			var missing = string.Join("-", tuple.Except([same]).Order());

			if (input.Contains(missing))
				cirles.Add(string.Join("-", tuple));
		}

		var result1 = cirles.Count(circle => circle.Split("-").Any(c => c.StartsWith("t")));
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}
}