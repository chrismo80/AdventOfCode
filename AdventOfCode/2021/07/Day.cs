using AdventOfCode;

namespace AdventOfCode2021;

public static class Day7
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<int>(",").Order();

		var result1 = Enumerable.Range(data.First(), data.Last() - data.First() + 1)
			.Min(t => data.Sum(x => Math.Abs(t - x)));
		var target1 = Enumerable.Range(data.First(), data.Last() - data.First() + 1)
			.First(t => data.Sum(x => Math.Abs(t - x)) == result1);

		yield return $"{result1} for target {target1}";

		var result2 = Enumerable.Range(data.First(), data.Last() - data.First() + 1)
			.Min(t => data.Sum(x => Enumerable.Range(1, Math.Abs(t - x)).Sum()));
		var target2 = Enumerable.Range(data.First(), data.Last() - data.First() + 1)
			.First(t => data.Sum(x => Enumerable.Range(1, Math.Abs(t - x)).Sum()) == result2);

		yield return $"{result2} for target {target2}";
	}
}