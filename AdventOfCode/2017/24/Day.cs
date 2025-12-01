using AdventOfCode;

namespace AdventOfCode2017;

public static class Day24
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<string>("\n")
			.Select(row => row.Split('/').Select(int.Parse))
			.Select(port => (port.First(), port.Last()));

		var bridges = Bridges(data, new List<(int, int)> { (0, 0) }).ToList();

		var maxLength = bridges.Max(bridge => bridge.Count());

		yield return bridges.Max(bridge => bridge.Strength());

		yield return bridges.Where(bridge => bridge.Count() == maxLength).Max(bridge => bridge.Strength());
	}

	private static int Strength(this IEnumerable<(int, int)> bridge) =>
		bridge.Sum(port => port.Item1 + port.Item2);

	private static int Match(this (int, int) last, (int, int) next) =>
		last.Item2 == next.Item1 ? 1 : last.Item2 == next.Item2 ? 2 : 0;

	private static IEnumerable<IEnumerable<(int, int)>> Bridges(IEnumerable<(int, int)> unused, IEnumerable<(int, int)> used)
	{
		foreach (var port in unused.Where(port => used.Last().Match(port) > 0))
		{
			var aligned = used.Last().Match(port) == 1 ? port : (port.Item2, port.Item1);

			yield return used.Append(aligned);

			unused = unused.Where(a => !a.Equals(port)).ToList();

			foreach (var segment in Bridges(unused, used.Append(aligned).ToList()))
				yield return segment;
		}
	}
}