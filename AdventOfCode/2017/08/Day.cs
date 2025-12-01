using AdventOfCode;

namespace AdventOfCode2017;

using Extensions;

public static class Day8
{
	public static IEnumerable<object> Solve(string input)
	{
		var instructions = input.Lines()
			.Select(row => row.Split(" if "))
			.Select(parts =>
			(
				Register: parts[0].Split(' ')[0],
				Action: parts[0].Split(' ').TakeLast(2).ToArray(),
				Condition: parts[1]
			));

		var registers = instructions.Select(inst => inst.Register).Distinct().ToDictionary(reg => reg, _ => 0);
		var result2 = 0;

		foreach (var inst in instructions)
		{
			var check = inst.Condition.Split(' ')[0];

			if (inst.Condition.Replace(check, registers[check].ToString()).Evaluate<bool>())
			{
				registers[inst.Register] += int.Parse(inst.Action[1]) * (inst.Action[0] == "inc" ? 1 : -1);
				result2 = Math.Max(result2, registers.Values.Max());
			}
		}

		yield return registers.Values.Max();
		yield return result2;
	}
}