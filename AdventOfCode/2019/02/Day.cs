namespace AdventOfCode2019;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input.Split(',').Select(int.Parse).ToArray();

		yield return RunProgram(program, 12, 2);

		var result2 = (from Noun in Enumerable.Range(0, 100)
			from Verb in Enumerable.Range(0, 100)
			where RunProgram(program, Noun, Verb) == 19690720
			select (Noun, Verb)).Single();

		yield return result2.Noun * 100 + result2.Verb;

		static int RunProgram(int[] memory, int noun, int verb)
		{
			var program = memory.Select(address => address).ToArray();

			program[1] = noun;
			program[2] = verb;

			for (var i = 0; i < program.Length; i += 4)
			{
				if (program[i] == 99)
					return program[0];

				if (program[i] == 1)
					program[program[i + 3]] = program[program[i + 1]] + program[program[i + 2]];

				if (program[i] == 2)
					program[program[i + 3]] = program[program[i + 1]] * program[program[i + 2]];
			}

			return -1;
		}
	}
}