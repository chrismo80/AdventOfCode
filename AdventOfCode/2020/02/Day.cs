using AdventOfCode;

namespace AdventOfCode2020;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines().Select(line =>
		(
			Left: int.Parse(line.Split(':')[0].Split(' ')[0].Split('-')[0]),
			Right: int.Parse(line.Split(':')[0].Split(' ')[0].Split('-')[1]),
			Char: line.Split(':')[0][^1..][0],
			Password: line.Split(": ")[1]
		)).ToList();

		yield return lines.Select(x => (Specs: x, Count: x.Password.Count(c => c == x.Char)))
			.Count(c => c.Count >= c.Specs.Left && c.Count <= c.Specs.Right);

		yield return lines.Count(p => (p.Password[p.Left - 1] == p.Char) ^ (p.Password[p.Right - 1] == p.Char));
	}
}