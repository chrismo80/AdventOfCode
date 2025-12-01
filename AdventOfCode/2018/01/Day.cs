using AdventOfCode;
using Extensions;

namespace AdventOfCode2018;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<int>("\n");

		yield return data.Sum();

		var frequency = 0;
		var history = new HashSet<int> { frequency };

		var lastChange = input.RepeatForever().SkipWhile(change => history.Add(frequency += change)).First();

		yield return frequency;
	}
}