namespace AdventOfCode2020;

public static class Day14
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = File.ReadAllLines("AdventOfCode/2020/14/Input.txt");

		yield return program.Run(1).Values.Sum();
		yield return program.Run(2).Values.Sum();
	}

	private static Dictionary<long, long> Run(this string[] program, int version)
	{
		long address, value;
		var memory = new Dictionary<long, long>();
		var mask = "";

		foreach (var line in program)
		{
			if (line.StartsWith("mask"))
			{
				mask = line.Split(" = ")[1];
				continue;
			}

			address = long.Parse(line.Split('[', ']')[1]);
			value = long.Parse(line.Split(" = ")[1]);

			switch (version)
			{
				case 1:
					memory[address] = value.ApplyV1(mask);
					break;
				case 2:
					foreach (var add in address.ApplyV2(mask))
						memory[add] = value;
					break;
			}
		}

		return memory;
	}

	private static long ApplyV1(this long value, string mask)
	{
		string masked = "", unmasked = Convert.ToString(value, 2).PadLeft(mask.Length, '0');

		for (var i = 1; i <= mask.Length; i++)
			masked = (mask[^i] == 'X' ? unmasked[^i] : mask[^i]) + masked;

		return Convert.ToInt64(masked, 2);
	}

	private static long[] ApplyV2(this long value, string mask)
	{
		var values = new List<long>();
		var permutations = new List<string>();
		string masked = "", unmasked = Convert.ToString(value, 2).PadLeft(mask.Length, '0');

		for (var i = 1; i <= mask.Length; i++)
			masked = (mask[^i] == '0' ? unmasked[^i] : mask[^i]) + masked;

		var positions = masked.Select((c, i) => (c, i)).Where(x => x.c == 'X').Select(x => x.i).ToArray();

		for (var i = 0; i < Math.Pow(2, positions.Length); i++)
			permutations.Add(Convert.ToString(i, 2).PadLeft(positions.Length, '0'));

		foreach (var floating in permutations)
		{
			for (var i = 0; i < floating.Length; i++)
				masked = masked.Remove(positions[i], 1).Insert(positions[i], $"{floating[i]}");

			values.Add(Convert.ToInt64(masked, 2));
		}

		return values.ToArray();
	}
}