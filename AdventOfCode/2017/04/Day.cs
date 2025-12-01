using AdventOfCode;

namespace AdventOfCode2017;

public static class Day4
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(row => row.Split(' '));

		yield return data.Count(p => p.Length == p.Distinct().Count());

		yield return data.Select(p => p.Select(w => new string(w.Order().ToArray())).ToArray())
			.Count(p => p.Length == p.Distinct().Count());
	}
}