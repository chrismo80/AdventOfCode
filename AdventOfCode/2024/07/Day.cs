using Extensions;

namespace AdventOfCode2024;

public static class Day7
{
	public static void Solve()
	{
		var input = Input.LoadLines(2024, 7)
			.Select(row =>
				(
					Test: long.Parse(row.Split(": ").First()),
					Values: row.Split(": ").Last().Split(' ').Select(long.Parse).ToArray()
				)
			).ToArray();

		var result1 = input.FindCalibrationResult('+', '*');
		var result2 = input.FindCalibrationResult('+', '*', '|');

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static long FindCalibrationResult(this IEnumerable<(long Test, long[] Values)> equations,
		params char[] symbols)
	{
		return equations.Where(equation => equation.Test.Assert(equation.Values, symbols))
			.Sum(equation => equation.Test);
	}

	private static bool Assert(this long test, long[] values, params char[] symbols)
	{
		return symbols.CartesianProduct(values.Length - 1)
			.Select(c => new string(c.ToArray()))
			.Any(variant => variant.Evaluate(values) == test);
	}

	private static long Evaluate(this string operands, params long[] values)
	{
		var value = values[0];

		for (var i = 1; i < values.Length; i++)
			switch (operands[i - 1])
			{
				case '+': value += values[i]; break;
				case '*': value *= values[i]; break;
				case '|': value = long.Parse($"{value}{values[i]}"); break;
			}

		return value;
	}
}