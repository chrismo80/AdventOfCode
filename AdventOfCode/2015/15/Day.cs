using AdventOfCode;

namespace AdventOfCode2015;

using Extensions;

public static class Day15
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines()
			.Select(row => row.Split(':', ',', ' ')
				.Where(word => int.TryParse(word, out var value)).Select(int.Parse))
			.ToArray();

		var result1 = new List<long>();
		var result2 = new List<long>();

		foreach (var recipe in from p0 in Enumerable.Range(0, 101)
				from p1 in Enumerable.Range(0, 101)
				from p2 in Enumerable.Range(0, 101)
				where p0 + p1 + p2 <= 100
				select new int[] { p0, p1, p2, 100 - p0 - p1 - p2 })
		{
			result1.Add(lines[0].Take(4).Select(value => value * recipe[0])
				.Zip(lines[1].Take(4).Select(value => value * recipe[1]), (x, y) => x + y)
				.Zip(lines[2].Take(4).Select(value => value * recipe[2]), (x, y) => x + y)
				.Zip(lines[3].Take(4).Select(value => value * recipe[3]), (x, y) => x + y)
				.Product(score => Math.Max(0, score)));

			if (lines.Select((p, i) => p.Last() * recipe[i]).Sum() == 500)
				result2.Add(result1.Last());
		}

		yield return result1.Max();
		yield return result2.Max();
	}
}