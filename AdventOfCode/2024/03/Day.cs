using System.Text.RegularExpressions;
using Extensions;

namespace AdventOfCode2024;

public static class Day3
{
	public static void Solve()
	{
		var input = Input.Load(2024, 3);

		var matches = new Regex(@"mul\((\d+)\,(\d+)\)|do\(\)|don\'t\(\)").Matches(input);

		var multiplications = matches.Where(match => match.Value.StartsWith("mul"));

		var enabled = true;
		var instructions = new List<Match>();

		foreach (Match match in matches)
			switch (match.Value)
			{
				case "do()":
					enabled = true;
					break;
				case "don't()":
					enabled = false;
					break;
				default:
					if (enabled)
						instructions.Add(match);
					break;
			}

		Console.WriteLine($"Part 1: {Calculate(multiplications)}, Part 2: {Calculate(instructions)}");
	}

	private static int Calculate(IEnumerable<Match> matches) =>
		matches.Select(match => match.Groups.Values.Skip(1).Select(g => int.Parse(g.Value)).Product())
			.Sum();
}