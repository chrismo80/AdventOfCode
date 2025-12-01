namespace AdventOfCode2019;

using System.Collections.Concurrent;

public static class Day21
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input.Split(',').Select(int.Parse).ToArray();

		var commands = new string[]
		{
			"NOT C J",
			"AND D J",
			"NOT A T",
			"OR T J",
			"WALK"
		};

		var springdroid = IntCodeComputer.Run(program, false);

		foreach (var command in commands)
		{
			foreach (var c in command)
				springdroid.Inputs.Add(c);
			springdroid.Inputs.Add(10);
		}

		var result = "";

		while (springdroid.Running || springdroid.Outputs.Any())
		{
			springdroid.Outputs.TryTake(out var value, 1000);
			result += (char)value;
		}

		yield return result;
	}

	public class IntCodeComputer
	{
		private readonly Task _program;
		public bool Running = true;
		public bool Waiting = true;
		public Dictionary<int, int> Memory { get; }
		public BlockingCollection<int> Inputs { get; set; } = new();
		public BlockingCollection<int> Outputs { get; set; } = new();
		public int Pointer { get; private set; }
		public int RelativeBase { get; private set; }

		public IntCodeComputer(int[] memory, bool wait)
		{
			Memory = Enumerable.Range(0, memory.Length).ToDictionary(i => i, i => memory[i]);
			_program = Task.Run(Start);
			if (wait) _program.Wait();
		}

		public static IntCodeComputer Run(int[] memory, bool wait = false) => new(memory, wait);

		private void Start()
		{
			while (Running)
				switch (Memory[Pointer] % 100)
				{
					case 1:
						Write(3, Read(1) + Read(2));
						Pointer += 4;
						break;
					case 2:
						Write(3, Read(1) * Read(2));
						Pointer += 4;
						break;
					case 3:
						Waiting = true;
						if (Inputs.TryTake(out var value, 5000))
						{
							Write(1, value);
							Pointer += 2;
							Waiting = false;
						}
						else
						{
							Running = false;
						}

						break;
					case 4:
						Outputs.Add(Read(1));
						Pointer += 2;
						break;
					case 5: Pointer = Read(1) != 0 ? Read(2) : Pointer + 3; break;
					case 6: Pointer = Read(1) == 0 ? Read(2) : Pointer + 3; break;
					case 7:
						Write(3, Read(1) < Read(2) ? 1 : 0);
						Pointer += 4;
						break;
					case 8:
						Write(3, Read(1) == Read(2) ? 1 : 0);
						Pointer += 4;
						break;
					case 9:
						RelativeBase += Read(1);
						Pointer += 2;
						break;
					case 99: Running = false; break;
				}

			Console.WriteLine("Stopped");
		}

		private int Read(int offset) => Memory.TryGetValue(Parameter(offset), out var value) ? value : 0;

		private void Write(int offset, int value)
		{
			Memory[Parameter(offset)] = value;
		}

		private int Parameter(int offset) =>
			Memory[Pointer].ToString().PadLeft(5, '0')[3 - offset] switch
			{
				'0' => Memory.TryGetValue(Pointer + offset, out var value) ? value : 0,
				'1' => Pointer + offset,
				'2' => (Memory.TryGetValue(Pointer + offset, out var value) ? value : 0) + RelativeBase,
				_ => 0
			};
	}
}