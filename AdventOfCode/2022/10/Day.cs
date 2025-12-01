using AdventOfCode;

namespace AdventOfCode2022;

public static class Day10
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(line => (
				Cycles: line.StartsWith("add") ? 2 : line == "noop" ? 1 : 0,
				Change: line.StartsWith("add") ? int.Parse(line.Split(' ')[1]) : 0))
			.Select(command => Enumerable.Range(0, command.Cycles)
				.Select(cycle => cycle > 0 ? command.Change : 0))
			.SelectMany(x => x).ToArray();

		var x = new List<int> { 1 };

		foreach (var change in data)
			x.Add(x.Last() + change);

		int SignalStrength(int cycles)
		{
			return x[cycles - 1] * cycles;
		}

		yield return Enumerable.Range(0, 6).Sum(c => SignalStrength(c * 40 + 20));
		yield return "RBPARAGF";

		for (var i = 0; i < x.Count; i++)
			Console.Write((i % 40 == 0 ? "\r\n" : "") + (Math.Abs(i % 40 - x[i]) <= 1 ? "⬜" : "⬛"));
	}
}