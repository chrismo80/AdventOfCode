namespace AdventOfCode2019;

public static class Day9
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input.Split(',').Select(long.Parse).ToArray();

		var iccs = Enumerable.Range(1, 2).Select(input => IntCodeComputer.Run(program, input, true)).ToArray();

		yield return iccs[0].GetOutput();
		yield return iccs[1].GetOutput();
	}

	public class IntCodeComputer
	{
		private readonly Task _program;
		public bool Running => _program.Status == TaskStatus.Running;
		public Dictionary<long, long> Memory { get; }
		public Queue<long> Inputs { get; set; } = new();
		public Queue<long> Outputs { get; set; } = new();

		public long Input
		{
			get => Inputs.Last();
			set => Inputs.Enqueue(value);
		}

		public long Pointer { get; private set; }
		public long RelativeBase { get; private set; }

		public IntCodeComputer(long[] memory, long input, bool wait)
		{
			Memory = Enumerable.Range(0, memory.Length).ToDictionary(i => (long)i, i => memory[i]);
			Input = input;
			_program = Task.Run(Start);
			if (wait) _program.Wait();
		}

		public static IntCodeComputer Run(long[] memory, long input, bool wait = false) => new(memory, input, wait);

		private void Start()
		{
			while (Memory[Pointer] != 99)
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
						if (Inputs.Count > 0)
						{
							Write(1, Inputs.Dequeue());
							Pointer += 2;
						}

						break;
					case 4:
						Outputs.Enqueue(Read(1));
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
				}
		}

		public long GetOutput()
		{
			while (!Outputs.Any())
			{ }

			return Outputs.Dequeue();
		}

		private long Read(int offset) => Memory.TryGetValue(Parameter(offset), out var value) ? value : 0;

		private void Write(int offset, long value)
		{
			Memory[Parameter(offset)] = value;
		}

		private long Parameter(int offset) =>
			Memory[Pointer].ToString().PadLeft(5, '0')[3 - offset] switch
			{
				'0' => Memory.TryGetValue(Pointer + offset, out var value) ? value : 0,
				'1' => Pointer + offset,
				'2' => (Memory.TryGetValue(Pointer + offset, out var value) ? value : 0) + RelativeBase,
				_ => 0L
			};
	}
}