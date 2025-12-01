using AdventOfCode;

namespace AdventOfCode2016;

public static class Day15
{
	public static IEnumerable<object> Solve(string input)
	{
		var discs = input.Lines()
			.Select(row => row.Split(' ', '#', '.'))
			.Select(data => (
				Number: int.Parse(data[2]),
				Positions: int.Parse(data[4]),
				Start: int.Parse(data[12])
			)).ToList();

		int FindWaitTime()
		{
			return Enumerable.Range(0, int.MaxValue).First(delay =>
				discs.All(disc => (disc.Number + disc.Start + delay) % disc.Positions == 0));
		}

		yield return FindWaitTime();

		discs.Add((discs.Max(disc => disc.Number + 1), 11, 0));

		yield return FindWaitTime();
	}
}