namespace AdventOfCode2019;
using System.Collections.Concurrent;
public static class Day13
{
    public static void Solve()
    {
        var program = File.ReadAllLines("AdventOfCode/2019/13/Input.txt")[0]
            .Split(',').Select(int.Parse).ToArray();

        var screen = IntCodeComputer.Run(program, true).Outputs.Chunk(3)
            .Select(tile => (X: tile[0], Y: tile[1], Id: tile[2]));

        Console.WriteLine($"Part 1: {screen.Count(tile => tile.Id == 2)}");

        var board = Enumerable.Range(0, screen.Max(tile => tile.Y) + 1)
            .Select(_ => Enumerable.Repeat(0, screen.Max(tile => tile.X) + 1).ToArray()).ToArray();

        program[0] = 2;
        var game = IntCodeComputer.Run(program, false);

        int paddle = 0, ball = 0, score = 0;

        while (game.Running || game.Outputs.Count >= 3)
        {
            game.Outputs.TryTake(out int X, 100);
            game.Outputs.TryTake(out int Y, 100);
            game.Outputs.TryTake(out int Id, 100);

            if (X >= 0)
                board[Y][X] = Id;
            else
                score = Id;

            if (Id == 4) paddle = X;
            if (Id == 3) ball = X;

            if (!game.Outputs.Any())
            {
                game.Inputs.Add(Math.Abs(paddle - ball) > 0 ? Math.Sign(paddle - ball) : 0);
                Thread.Sleep(50);
                Print(board, "AdventOfCode/2019/13/Output.txt");
            }
        }
        Console.WriteLine($"Part 2: {score}");
    }

    static void Print(int[][] grid, string file, int sleep = 0)
    {
        Thread.Sleep(sleep);
        var output = new List<string>();

        for (int y = 0; y < grid.Length; y++)
        {
            var row = new List<char>();

            for (int x = 0; x < grid[0].Length; x++)
                row.Add(grid[y][x] switch { 1 => '|', 2 => '#', 3 => '-', 4 => 'o', _ => '.', });

            output.Add(new string(row.ToArray()));
        }
        File.WriteAllLines(file, output);
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
                        if (Inputs.TryTake(out int value, 10000))
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
            Console.WriteLine("Stopped");
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
