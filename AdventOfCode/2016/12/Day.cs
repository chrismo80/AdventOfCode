using AdventOfCode;

namespace AdventOfCode2016;

public static class Day12
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input.Lines();

		yield return Run(0);
		yield return Run(1);

		int Run(int start, int i = 0)
		{
			var registers = new Dictionary<char, int>
			{
				{ 'a', 0 },
				{ 'b', 0 },
				{ 'c', start },
				{ 'd', 0 }
			};

			while (i < program.Length)
			{
				var cmd = program[i].Split(' ');

				switch (cmd[0])
				{
					case "cpy":
						registers[cmd[2][0]] = registers.TryGetValue(cmd[1][0], out var val) ? val : int.Parse(cmd[1]);
						i++;
						break;
					case "inc":
						registers[cmd[1][0]]++;
						i++;
						break;
					case "dec":
						registers[cmd[1][0]]--;
						i++;
						break;
					case "jnz":
						var compare = registers.TryGetValue(cmd[1][0], out var value) ? value : int.Parse(cmd[1]);
						i += compare != 0 ? int.Parse(cmd[2]) : 1;
						break;
				}
			}

			return registers['a'];
		}
	}
}