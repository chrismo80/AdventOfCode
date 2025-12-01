using AdventOfCode;

namespace AdventOfCode2017;

public static class Day23
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input.ToArray<string>("\n");

		yield return Run(0);

		long Run(int start = 0)
		{
			int i = 0, mul = 0, c = 0;
			long value, compare;
			var registers = Enumerable.Range(0, 8).ToDictionary(i => (char)('a' + i), _ => 0L);
			registers['a'] = start;

			while (i < program.Length && c < 100_000_000)
			{
				if (c++ % 10_000_000 == 0) Console.WriteLine(string.Join('\t', registers));

				var cmd = program[i].Split(' ');

				value = registers.TryGetValue(cmd[2][0], out value) ? value : int.Parse(cmd[2]);
				compare = registers.TryGetValue(cmd[1][0], out compare) ? compare : int.Parse(cmd[1]);

				mul += cmd[0] == "mul" ? 1 : 0;

				switch (cmd[0])
				{
					case "set":
						registers[cmd[1][0]] = value;
						i++;
						break;
					case "sub":
						registers[cmd[1][0]] -= value;
						i++;
						break;
					case "mul":
						registers[cmd[1][0]] *= value;
						i++;
						break;
					case "jnz": i += compare != 0 ? int.Parse(cmd[2]) : 1; break;
				}
			}

			return start == 0 ? mul : registers['h'];
		}
	}
}