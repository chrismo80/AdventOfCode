namespace AdventOfCode2019;
using System.Collections.Concurrent;
public static class Day11
{
    public static void Solve()
    {
        var program = File.ReadAllLines("AdventOfCode/2019/11/Input.txt")[0]
            .Split(',').Select(long.Parse).ToArray();

        var robot = IntCodeComputer.Run(program);
        var trail = new HashSet<(int X, int Y)>();
        var hull = new HashSet<(int X, int Y)>(); // { (0, 0) }; Part 2
        var (X, Y) = (0, 0);
        int view = 0;

        while (robot.Running)
        {
            trail.Add((X, Y));
            robot.Inputs.Add(hull.Contains((X, Y)) ? 1 : 0);

            if (robot.Outputs.TryTake(out long color, 10))
            {
                if (color == 1) hull.Add((X, Y));
                else hull.Remove((X, Y));
            }
            else { break; }

            if (robot.Outputs.TryTake(out long direction, 10))
            {
                view = (view + 4 + (direction == 1 ? 1 : -1)) % 4;

                X = view == 1 ? X + 1 : view == 3 ? X - 1 : X;
                Y = view == 2 ? Y + 1 : view == 0 ? Y - 1 : Y;
            }
            else { break; }
        }

        Print(hull);

        Console.WriteLine($"Part 1: {trail.Count}");
    }

    static void Print(HashSet<(int X, int Y)> hull)
    {
        var output = new List<string>();

        for (int y = 0; y <= hull.Max(w => w.Y); y++)
        {
            var row = new List<char>();

            for (int x = hull.Min(w => w.X); x <= hull.Max(w => w.X); x++)
                row.Add(hull.Contains((x, y)) ? '#' : '.');

            output.Add(new string(row.ToArray()));
        }

        File.WriteAllLines("AdventOfCode/2019/11/Output.txt", output);
    }

    public class IntCodeComputer
    {
        readonly Task _program;
        public bool Running = true;
        public Dictionary<long, long> Memory { get; }
        public BlockingCollection<long> Inputs { get; set; } = new BlockingCollection<long>();
        public BlockingCollection<long> Outputs { get; set; } = new BlockingCollection<long>();
        public long Pointer { get; private set; }
        public long RelativeBase { get; private set; }

        public IntCodeComputer(long[] memory, bool wait)
        {
            Memory = Enumerable.Range(0, memory.Length).ToDictionary(i => (long)i, i => memory[i]);
            _program = Task.Run(Start); if (wait) _program.Wait();
        }

        public static IntCodeComputer Run(long[] memory, bool wait = false) => new(memory, wait);

        private void Start()
        {
            while (Memory[Pointer] != 99)
            {
                switch (Memory[Pointer] % 100)
                {
                    case 1: Write(3, Read(1) + Read(2)); Pointer += 4; break;
                    case 2: Write(3, Read(1) * Read(2)); Pointer += 4; break;
                    case 3:
                        if (Inputs.TryTake(out long value, 10000)) { Write(1, value); Pointer += 2; }
                        else { throw new TimeoutException("Input"); }
                        break;
                    case 4: Outputs.Add(Read(1)); Pointer += 2; break;
                    case 5: Pointer = Read(1) != 0 ? Read(2) : Pointer + 3; break;
                    case 6: Pointer = Read(1) == 0 ? Read(2) : Pointer + 3; break;
                    case 7: Write(3, Read(1) < Read(2) ? 1 : 0); Pointer += 4; break;
                    case 8: Write(3, Read(1) == Read(2) ? 1 : 0); Pointer += 4; break;
                    case 9: RelativeBase += Read(1); Pointer += 2; break;
                }
            }
            Running = false;
        }

        private long Read(int offset) => Memory.TryGetValue(Parameter(offset), out long value) ? value : 0;

        private void Write(int offset, long value) { Memory[Parameter(offset)] = value; }

        private long Parameter(int offset) =>
            Memory[Pointer].ToString().PadLeft(5, '0')[3 - offset] switch
            {
                '0' => Memory.TryGetValue(Pointer + offset, out long value) ? value : 0,
                '1' => Pointer + offset,
                '2' => (Memory.TryGetValue(Pointer + offset, out long value) ? value : 0) + RelativeBase,
                _ => 0L,
            };
    }
}