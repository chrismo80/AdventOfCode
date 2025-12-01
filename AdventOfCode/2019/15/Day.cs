namespace AdventOfCode2019;

using System.Collections.Concurrent;

public static class Day15
{
	public static IEnumerable<object> Solve(string input)
	{
		var program = input
			.Split(',').Select(int.Parse).ToArray();

		var walls = new HashSet<(int X, int Y)>();
		var free = new HashSet<(int X, int Y)>();

		var pos = (X: 0, Y: 0);
		var next = pos;
		var oxygen = next; //(X: -16, Y: -12);
		var dir = 0;
		var rand = new Random();

		var droid = IntCodeComputer.Run(program, false);

		while (droid.Running && free.Count + walls.Count < 1658)
		{
			var look = 0;
			while ((free.Contains(next) || walls.Contains(next)) && look++ < 3)
			{
				dir = (dir + rand.Next(100)) % 4;
				next.X = dir == 0 || dir == 1 ? pos.X : dir == 3 ? pos.X - 1 : pos.X + 1;
				next.Y = dir == 2 || dir == 3 ? pos.Y : dir == 0 ? pos.Y - 1 : pos.Y + 1;
			}

			droid.Inputs.Add(dir + 1);
			droid.Outputs.TryTake(out var status, 100);

			switch (status)
			{
				case 0: walls.Add(next); break;
				case 1:
					free.Add(next);
					pos = next;
					break;
				case 2:
					free.Add(next);
					pos = next;
					oxygen = pos;
					break;
			}
		}

		PrintMap(walls, free, pos);

		var start = (0 - walls.Min(w => w.X), 0 - walls.Min(w => w.Y));
		oxygen = (oxygen.X - walls.Min(w => w.X), oxygen.Y - walls.Min(w => w.Y));

		var search = new PathFinding.Grid<char>
		{
			Map = PrintMap(walls, free, pos).Select(row => row.ToArray()).ToArray(),
			Walkable = (_, _, next, _, _, _, _, _, _) => next != '#'
		};

		var result1 = search.BreadthFirstSearch(start, oxygen).ToList();
		Console.WriteLine(search.Print('⭕', '⚪', '❕', '❔'));

		var result2 = search.BreadthFirstSearch(oxygen, (0, 0)).ToList();
		Console.WriteLine(search.Print('⭕', '⚪'));

		yield return result1.Count;
		yield return result2.Count;
	}

	private static IEnumerable<string> PrintMap(
		HashSet<(int X, int Y)> walls, HashSet<(int X, int Y)> free, (int X, int Y) droid)
	{
		var output = new List<string>();

		for (var y = walls.Min(w => w.Y); y <= walls.Max(w => w.Y); y++)
		{
			var row = new List<char>();

			for (var x = walls.Min(w => w.X); x <= walls.Max(w => w.X); x++)
				row.Add((x, y) == (0, 0) ? 'S' :
					(x, y) == droid ? 'D' :
					walls.Contains((x, y)) ? '#' :
					free.Contains((x, y)) ? '.' : ' ');

			output.Add(new string(row.ToArray()));
		}

		return output;
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
						if (Inputs.TryTake(out var value, 100))
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