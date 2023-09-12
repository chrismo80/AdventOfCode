namespace AdventOfCode2018;

public static class Day17
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/17/Test.txt")
            .Select(line => line.Split(", ").Order()
                .Select(axis => new string(axis.Skip(2).ToArray()))
                .Select(pos => pos.Contains("..") ? pos : $"{pos}..{pos}")
                .Select(pos => pos.Split("..").Select(int.Parse).ToArray())
                .ToArray())
            .ToArray()
            .Select(c => (From: (X: c[0][0], Y: c[1][0]), To: (X: c[0][1], Y: c[1][1])))
            .ToArray();

        var walls = new HashSet<(int X, int Y)>();

        foreach (var row in input)
        {
            var wall = from X in
                Enumerable.Range(Math.Min(row.From.X, row.To.X), Math.Abs(row.From.X - row.To.X) + 1)
                       from Y in
                Enumerable.Range(Math.Min(row.From.Y, row.To.Y), Math.Abs(row.From.Y - row.To.Y) + 1)
                       select (X, Y);

            foreach (var brick in wall)
                walls.Add(brick);
        }

        var water = new HashSet<(int X, int Y)>();
        var drop = (X: 500, Y: 0);

        var baisin = new List<(int X, int Y)>();

        (int X, int Y) LeftOf((int X, int Y) drop) => (drop.X - 1, drop.Y);
        (int X, int Y) RightOf((int X, int Y) drop) => (drop.X + 1, drop.Y);
        (int X, int Y) DownOf((int X, int Y) drop) => (drop.X, drop.Y + 1);

        bool Occupied((int X, int Y) drop) => walls.Contains(drop) || water.Contains(drop);

        while (!water.Contains(drop) && walls.Max(w => w.Y) > drop.Y)
        {
            water.Add(drop); PrintMap(walls, water); water.Remove(drop);

            drop.Y++;
            if (!Occupied(drop)) continue;
            drop.Y--;

            if (!Occupied(LeftOf(drop)))
            {
                while (!Occupied(LeftOf(drop)))
                {
                    drop.X--;
                    if (!Occupied(DownOf(drop))) break;
                }

                if (Occupied(LeftOf(drop)))
                {
                    water.Add(drop);
                    drop = (500, 0);
                    continue;
                }
            }

            if (!Occupied(RightOf(drop)))
            {
                while (!Occupied(RightOf(drop)))
                {
                    drop.X++;
                    if (!Occupied(DownOf(drop))) break;
                }

                if (Occupied(RightOf(drop)))
                {
                    water.Add(drop);
                    drop = (500, 0);
                    continue;
                }
            }

            water.Add(drop);
            drop = (500, 0);
        }

        Console.WriteLine(water.Count);

        PrintMap(walls, water);
    }

    static void PrintMap(HashSet<(int X, int Y)> walls, HashSet<(int X, int Y)> water)
    {
        Thread.Sleep(100);
        var output = new List<string>();

        for (int y = 0; y <= walls.Max(w => w.Y); y++)
        {
            var row = new List<char>();

            for (int x = walls.Min(w => w.X); x <= walls.Max(w => w.X); x++)
                row.Add(walls.Contains((x, y)) ? '#' : water.Contains((x, y)) ? '~' : '.');

            output.Add(new string(row.ToArray()));
        }

        File.WriteAllLines("AdventOfCode/2018/17/Output.txt", output);
    }
}