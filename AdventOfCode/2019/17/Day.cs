namespace AdventOfCode2019;

using System.Collections.Concurrent;

public static class Day17
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input.Split(',').Select(int.Parse).ToArray();

		var scaffold = new HashSet<(int X, int Y)>();
		var (X, Y) = (0, 0);

		program[0] = 2;
		var robot = IntCodeComputer.Run(program, false);

		var commands = new string[]
		{
			"A,B,A,C,A,B,C,B,C,B",
			"R,8,L,10,L,12,R,4",
			"R,8,L,12,R,4,R,4",
			"R,8,L,10,R,8",
			"n"
		};

		foreach (var command in commands)
		{
			foreach (var c in command)
				robot.Inputs.Add(c);
			robot.Inputs.Add(10);
		}

		while (robot.Running)
		{
			robot.Outputs.TryTake(out var value, 1000);
			Console.Write((char)value);

			switch (value)
			{
				case 10:
					X = -1;
					Y++;
					break;
				case 35:
					if (Y < 50) scaffold.Add((X, Y));
					break;
			}

			X++;
		}

		yield return scaffold.Where(location => IsIntersection(location)).Sum(i => i.X * i.Y);
		yield return robot.Outputs.Last();

		bool IsIntersection((int X, int Y) location)
		{
			return scaffold.Contains((location.X + 1, location.Y)) &&
				scaffold.Contains((location.X - 1, location.Y)) &&
				scaffold.Contains((location.X, location.Y + 1)) &&
				scaffold.Contains((location.X, location.Y - 1));
		}
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