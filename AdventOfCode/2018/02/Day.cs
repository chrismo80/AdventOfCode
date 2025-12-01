using AdventOfCode;

namespace AdventOfCode2018;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		int Count(int match)
		{
			return lines.Count(box => box.Select(c => box.Count(x => x == c)).Contains(match));
		}

		yield return Count(2) * Count(3);

		yield return Enumerable.Range(0, lines[0].Length)
			.Select(position => lines.Select(box => box.Remove(position, 1))
				.GroupBy(box => box).Where(group => group.Count() > 1).Select(group => group.Key)) // find duplicates
			.Single(l => l.Any()).Single();
	}
}