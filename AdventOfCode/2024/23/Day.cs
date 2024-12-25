using AdventOfCode;

namespace AdventOfCode2024;

public static class Day23
{
	public static void Solve()
	{
		var input = Input.Load(2024, 23)
			.ToNestedArray<string>("\n", "-")
			.Select(c => c.Order().ToArray())
			.Distinct()
			.ToArray();

		var cirles = new HashSet<string>();

		var i = 1;
		foreach (var c1 in input)
		{
			Console.WriteLine(i++);

			foreach (var c2 in input)
			{
				if (c1.SequenceEqual(c2))
					continue;

				foreach (var c3 in input)
				{
					if (c2.SequenceEqual(c3))
						continue;

					var list = c1.Concat(c2).Concat(c3).Distinct().ToArray();

					if (list.Count() == 3)
						cirles.Add(string.Join("-", list.Order()));
				}
			}
		}

		var result1 = cirles.Count(circle => circle.Split("-").Any(c => c.StartsWith("t")));
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}
}