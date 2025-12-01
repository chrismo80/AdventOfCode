namespace AdventOfCode2018;

public static class Day16
{
	public static IEnumerable<object> Solve(string input)
	{
		var parts = input.Split("\n\n\n\n");

		var examples = parts[0].Split("\n\n")
			.Select(row => row.Split('[', ']', ',', ' ', '\n')
				.Where(item => int.TryParse(item, out var value)).Select(int.Parse))
			.Select(n =>
			(
				Before: n.Take(4).ToArray(),
				Instruction: n.Skip(4).Take(4).ToArray(),
				After: n.TakeLast(4).ToArray()
			))
			.ToArray();

		var result1 = examples.Select(ex => OpCodeMatch(ex).Count()).Count(matches => matches >= 3);

		var testProgram = parts[1].Split('\n').Select(row => row.Split(' ').Select(int.Parse).ToArray());

		var registers = new int[4];

		foreach (var instruction in testProgram)
			registers = Apply(instruction, registers);

		yield return result1;
		yield return registers[0];
	}

	private static IEnumerable<int> OpCodeMatch((int[] Before, int[] Instruction, int[] After) example)
	{
		var instr = example.Instruction.Select(i => i).ToArray();

		for (var i = 0; i <= 15; i++)
		{
			instr[0] = i;

			if (Apply(instr, example.Before).SequenceEqual(example.After))
				yield return instr[0];
		}
	}

	private static int[] Apply(int[] instruction, int[] input)
	{
		var output = input.Select(r => r).ToArray();

		switch (instruction[0])
		{
			case 0: output[instruction[3]] = input[instruction[1]] & instruction[2]; break;
			case 1: output[instruction[3]] = input[instruction[1]] > instruction[2] ? 1 : 0; break;
			case 2: output[instruction[3]] = instruction[1]; break;
			case 4: output[instruction[3]] = input[instruction[1]] == input[instruction[2]] ? 1 : 0; break;
			case 3: output[instruction[3]] = instruction[1] == input[instruction[2]] ? 1 : 0; break;
			case 5: output[instruction[3]] = input[instruction[1]] | input[instruction[2]]; break;
			case 6: output[instruction[3]] = input[instruction[1]] | instruction[2]; break;
			case 7: output[instruction[3]] = input[instruction[1]] & input[instruction[2]]; break;
			case 8: output[instruction[3]] = input[instruction[1]] * instruction[2]; break;
			case 9: output[instruction[3]] = input[instruction[1]] == instruction[2] ? 1 : 0; break;
			case 10: output[instruction[3]] = input[instruction[1]] * input[instruction[2]]; break;
			case 11: output[instruction[3]] = input[instruction[1]] > input[instruction[2]] ? 1 : 0; break;
			case 12: output[instruction[3]] = input[instruction[1]]; break;
			case 13: output[instruction[3]] = input[instruction[1]] + input[instruction[2]]; break;
			case 14: output[instruction[3]] = instruction[1] > input[instruction[2]] ? 1 : 0; break;
			case 15: output[instruction[3]] = input[instruction[1]] + instruction[2]; break;
		}

		return output;
	}
}