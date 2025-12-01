using AdventOfCode;

namespace AdventOfCode2021;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(x => int.Parse(x));

		yield return data
			.Aggregate((Value: 0, Counter: 0), (last, current) =>
			(
				Value: current,
				Counter: last.Value < current ? last.Counter + 1 : last.Counter
			)).Counter - 1;

		yield return Enumerable.Range(0, data.Count() - 2)
			.Select(i => data.Skip(i).Take(3).Sum())
			.Aggregate((Value: 0, Counter: 0), (last, current) =>
			(
				Value: current,
				Counter: last.Value < current ? last.Counter + 1 : last.Counter
			)).Counter - 1;
	}
}