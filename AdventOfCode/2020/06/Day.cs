namespace AdventOfCode2020;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		var parts = input.Split("\n\n");

		yield return parts.Sum(g => g.Replace("\n", "").Distinct().Count());
		yield return parts.Sum(g => g.Split("\n").Aggregate((p1, p2) => new string(p1.Intersect(p2).ToArray())).Length);
	}
}