namespace AdventOfCode2019;
using System.Collections.Concurrent;
public static class Day19
{
    public static void Solve()
    {
        var program = File.ReadAllLines("AdventOfCode/2019/19/Input.txt")[0]
            .Split(',').Select(int.Parse).ToArray();

        var beam = new HashSet<(int X, int Y)>();

        int x = 0, y = 0, last = 0, startX = 0, ship = 100;
        var result = 0;

        while (result == 0)
        {
            var droneSystem = IntCodeComputer.Run(program, false);

            droneSystem.Inputs.Add(x);
            droneSystem.Inputs.Add(y);

            droneSystem.Outputs.TryTake(out int value, 1000);

            //Console.WriteLine($"{x}, {y}: {value}");

            if (value == 1)
                beam.Add((x, y));

            if (beam.Count(b => b.X == x) >= ship && beam.Count(b => b.Y == y) >= ship)
                result = ((x - ship + 1) * 10000) + (y - ship + 1);

            if ((value == 0 && last == 1) || (!beam.Any(b => b.X == x) && x > 10))
            {
                y++;
                x = Math.Max(startX - 5, 0);
            }

            if (value == 1 && last == 0)
                startX = x;

            x++;
            last = value;
        }

        Console.WriteLine(beam.Count);
        Console.WriteLine(result);

        //Print();

        void Print()
        {
            var output = new List<string>();

            for (int y = beam.Min(w => w.Y); y <= beam.Max(w => w.Y); y++)
            {
                var row = new List<char>();

                for (int x = beam.Min(w => w.X); x <= beam.Max(w => w.X); x++)
                    row.Add(beam.Contains((x, y)) ? '#' : '.');

                output.Add(new string(row.ToArray()));
            }

            File.WriteAllLines("AdventOfCode/2019/19/Output.txt", output);
        }
    }

    public class IntCodeComputer
    {
        readonly Task _program;
        public bool Running = true;
        public bool Waiting = true;
        public Dictionary<int, int> Memory { get; }
        public BlockingCollection<int> Inputs { get; set; } = new BlockingCollection<int>();
        public BlockingCollection<int> Outputs { get; set; } = new BlockingCollection<int>();
        public int Pointer { get; private set; }
        public int RelativeBase { get; private set; }

        public IntCodeComputer(int[] memory, bool wait)
        {
            Memory = Enumerable.Range(0, memory.Length).ToDictionary(i => i, i => memory[i]);
            _program = Task.Run(Start); if (wait) _program.Wait();
        }

        public static IntCodeComputer Run(int[] memory, bool wait = false) => new(memory, wait);

        private void Start()
        {
            while (Running)
            {
                switch (Memory[Pointer] % 100)
                {
                    case 1: Write(3, Read(1) + Read(2)); Pointer += 4; break;
                    case 2: Write(3, Read(1) * Read(2)); Pointer += 4; break;
                    case 3:
                        Waiting = true;
                        if (Inputs.TryTake(out int value, 5000))
                        { Write(1, value); Pointer += 2; Waiting = false; }
                        else
                        { Running = false; }
                        break;
                    case 4: Outputs.Add(Read(1)); Pointer += 2; break;
                    case 5: Pointer = Read(1) != 0 ? Read(2) : Pointer + 3; break;
                    case 6: Pointer = Read(1) == 0 ? Read(2) : Pointer + 3; break;
                    case 7: Write(3, Read(1) < Read(2) ? 1 : 0); Pointer += 4; break;
                    case 8: Write(3, Read(1) == Read(2) ? 1 : 0); Pointer += 4; break;
                    case 9: RelativeBase += Read(1); Pointer += 2; break;
                    case 99: Running = false; break;
                }
            }
            //Console.WriteLine("Stopped");
        }

        private int Read(int offset) => Memory.TryGetValue(Parameter(offset), out int value) ? value : 0;

        private void Write(int offset, int value) { Memory[Parameter(offset)] = value; }

        private int Parameter(int offset) =>
            Memory[Pointer].ToString().PadLeft(5, '0')[3 - offset] switch
            {
                '0' => Memory.TryGetValue(Pointer + offset, out int value) ? value : 0,
                '1' => Pointer + offset,
                '2' => (Memory.TryGetValue(Pointer + offset, out int value) ? value : 0) + RelativeBase,
                _ => 0,
            };
    }
}