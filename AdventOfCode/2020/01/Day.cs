using AdventOfCode;

namespace AdventOfCode2020;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<int>("/n");

		var result1 = (from x in data
			from y in data
			where x + y == 2020
			select x * y).First();

		var result2 = (from x in data
			from y in data
			from z in data
			where x + y + z == 2020
			select x * y * z).First();

		yield return result1;
		yield return result2;
	}
}