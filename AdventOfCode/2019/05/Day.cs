namespace AdventOfCode2019;

public static class Day5
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input.Split(',').Select(int.Parse).ToArray();

		yield return Run(program, 1);
		yield return Run(program, 5);

		static int Run(int[] memory, int input)
		{
			var program = memory.Select(address => address).ToArray();
			int i = 0, output = 0;

			while (program[i] != 99)
			{
				if (program[i].ToString().EndsWith("1"))
				{
					var mode1 = program[i].ToString().PadLeft(5, '0')[2];
					var mode2 = program[i].ToString().PadLeft(5, '0')[1];
					var mode3 = program[i].ToString().PadLeft(5, '0')[0];

					var param1 = mode1 == '0' ? program[program[i + 1]] : program[i + 1];
					var param2 = mode2 == '0' ? program[program[i + 2]] : program[i + 2];

					program[program[i + 3]] = param1 + param2;
					i += 4;
				}

				if (program[i].ToString().EndsWith("2"))
				{
					var mode1 = program[i].ToString().PadLeft(5, '0')[2];
					var mode2 = program[i].ToString().PadLeft(5, '0')[1];

					var param1 = mode1 == '0' ? program[program[i + 1]] : program[i + 1];
					var param2 = mode2 == '0' ? program[program[i + 2]] : program[i + 2];

					program[program[i + 3]] = param1 * param2;
					i += 4;
				}

				if (program[i] == 3)
				{
					program[program[i + 1]] = input;
					i += 2;
				}

				if (program[i].ToString().EndsWith("4"))
				{
					var mode1 = program[i].ToString().PadLeft(5, '0')[2];
					output = mode1 == '0' ? program[program[i + 1]] : program[i + 1];
					i += 2;
				}

				if (input == 1)
					continue;

				if (program[i].ToString().EndsWith("5"))
				{
					var mode1 = program[i].ToString().PadLeft(5, '0')[2];
					var mode2 = program[i].ToString().PadLeft(5, '0')[1];

					var param1 = mode1 == '0' ? program[program[i + 1]] : program[i + 1];
					var param2 = mode2 == '0' ? program[program[i + 2]] : program[i + 2];

					i = param1 != 0 ? param2 : i + 3;
				}

				if (program[i].ToString().EndsWith("6"))
				{
					var mode1 = program[i].ToString().PadLeft(5, '0')[2];
					var mode2 = program[i].ToString().PadLeft(5, '0')[1];

					var param1 = mode1 == '0' ? program[program[i + 1]] : program[i + 1];
					var param2 = mode2 == '0' ? program[program[i + 2]] : program[i + 2];

					i = param1 == 0 ? param2 : i + 3;
				}

				if (program[i].ToString().EndsWith("7"))
				{
					var mode1 = program[i].ToString().PadLeft(5, '0')[2];
					var mode2 = program[i].ToString().PadLeft(5, '0')[1];

					var param1 = mode1 == '0' ? program[program[i + 1]] : program[i + 1];
					var param2 = mode2 == '0' ? program[program[i + 2]] : program[i + 2];

					program[program[i + 3]] = param1 < param2 ? 1 : 0;
					i += 4;
				}

				if (program[i].ToString().EndsWith("8"))
				{
					var mode1 = program[i].ToString().PadLeft(5, '0')[2];
					var mode2 = program[i].ToString().PadLeft(5, '0')[1];

					var param1 = mode1 == '0' ? program[program[i + 1]] : program[i + 1];
					var param2 = mode2 == '0' ? program[program[i + 2]] : program[i + 2];

					program[program[i + 3]] = param1 == param2 ? 1 : 0;
					i += 4;
				}
			}

			return output;
		}
	}
}