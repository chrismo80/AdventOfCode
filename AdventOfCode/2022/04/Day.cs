using AdventOfCode;

namespace AdventOfCode2022;

public static class Day4
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(l => l.Split(",")
				.Select(e => e.Split("-").Select(int.Parse))
				.Select(r => Enumerable.Range(r.First(), r.Last() - r.First() + 1))
				.OrderBy(r => r.Count()));

		yield return data.Count(e => e.First().Intersect(e.Last()).Count() == e.First().Count());
		yield return data.Count(e => e.First().Intersect(e.Last()).Any());
	}
}