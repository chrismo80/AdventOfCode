using AdventOfCode;

namespace AdventOfCode2017;

public static class Day5
{
	public static IEnumerable<object> Solve(string input)
	{
		var numbers = input.ToArray<int>("\n");

		int pointer = 0, steps = 0, change;

		while (pointer < numbers.Length)
		{
			change = numbers[pointer] >= 3 ? -1 : 1; // 1;
			numbers[pointer] += change;
			pointer += numbers[pointer] - change;
			steps++;
		}

		yield return steps;
	}
}