namespace AdventOfCode2020;

public static class Day15
{
	public static IEnumerable<object> Solve(string input)
	{
		var spoken = input.Split(',').Select(int.Parse).ToList();

		while (spoken.Count < 2020)
			spoken.Add(spoken.Count(s => s == spoken.Last()) == 1 ? 0 :
				spoken.Count - spoken.LastIndexOf(spoken.Last(), spoken.Count - 2) - 1);

		yield return spoken.Last();
	}
}