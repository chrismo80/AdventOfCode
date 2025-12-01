using AdventOfCode;

namespace AdventOfCode2015;

using System.Text.RegularExpressions;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		var reg = new Regex("(.*) (\\d+),(\\d+) through (\\d+),(\\d+)");

		var lines = input.Lines().Select(line => (
			Action: reg.Match(line).Groups[1].Value,
			X1: int.Parse(reg.Match(line).Groups[2].Value),
			Y1: int.Parse(reg.Match(line).Groups[3].Value),
			X2: int.Parse(reg.Match(line).Groups[4].Value),
			Y2: int.Parse(reg.Match(line).Groups[5].Value)
		));

		var grid1 = Enumerable.Range(0, 1000)
			.Select(_ => Enumerable.Range(0, 1000).Select(_ => 0).ToArray())
			.ToArray();

		var grid2 = Enumerable.Range(0, 1000)
			.Select(_ => Enumerable.Range(0, 1000).Select(_ => 0).ToArray())
			.ToArray();

		foreach (var cmd in lines)
			for (var x = cmd.X1; x <= cmd.X2; x++)
			for (var y = cmd.Y1; y <= cmd.Y2; y++)
			{
				grid1[x][y] = cmd.Action == "toggle" ? 1 - grid1[x][y] : cmd.Action == "turn on" ? 1 : 0;
				grid2[x][y] += cmd.Action == "toggle" ? 2 : cmd.Action == "turn on" ? 1 : -1;
				grid2[x][y] = Math.Max(grid2[x][y], 0);
			}

		yield return grid1.SelectMany(x => x).Count(x => x > 0);
		yield return grid2.SelectMany(x => x).Sum(x => x);
	}
}