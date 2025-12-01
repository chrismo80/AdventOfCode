using AdventOfCode;

namespace AdventOfCode2022;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		yield return lines
			.Select(line => line.Chunk(line.Length / 2))
			.Select(pack => pack.First().Intersect(pack.Last()).Single())
			.Sum(item => char.IsUpper(item) ? item - 'A' + 27 : item - 'a' + 1);

		yield return lines.Chunk(3)
			.Select(group => group[0].Intersect(group[1]).Intersect(group[2]).Single())
			.Sum(item => char.IsUpper(item) ? item - 'A' + 27 : item - 'a' + 1);
	}
}