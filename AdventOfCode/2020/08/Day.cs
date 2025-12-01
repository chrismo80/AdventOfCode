using AdventOfCode;

namespace AdventOfCode2020;

public static class Day8
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(l => l.Split(' ')).ToArray();

		RunProgram(data, out var acc1);

		var inverted = new List<int>();
		var program = data;
		int acc2;

		while (!RunProgram(program, out acc2))
			for (var i = 0; i < program.Length; i++)
				if (program[i][0] != "acc" && !inverted.Contains(i))
				{
					program = data.Select(cmd => cmd.Select(x => x).ToArray()).ToArray();
					program[i][0] = program[i][0] == "jmp" ? "nop" : "jmp";
					inverted.Add(i);
					break;
				}

		yield return acc1;
		yield return acc2;

		static bool RunProgram(string[][] program, out int acc)
		{
			acc = 0;
			var pointer = 0;
			var executed = new List<int>();

			while (pointer < program.Length)
			{
				executed.Add(pointer);
				acc += program[pointer][0] == "acc" ? int.Parse(program[pointer][1]) : 0;
				pointer += program[pointer][0] == "jmp" ? int.Parse(program[pointer][1]) : 1;

				if (executed.Contains(pointer))
					return false;
			}

			return true;
		}
	}
}