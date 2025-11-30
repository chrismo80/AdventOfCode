using AdventOfCode;

namespace AdventOfCode2025;

public static class Day1
{
	public static void Solve()
	{
		var input = Input.Load(2025, 1).ToArray<int>("\n");

		var result = input.Sum();

		Console.WriteLine($"Part 1: {result}, Part 2: {result}");
	}
}