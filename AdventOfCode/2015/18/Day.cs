namespace AdventOfCode2015;
public static class Day18
{
    public static void Solve()
    {
        var grid = File.ReadAllLines("AdventOfCode/2015/18/Input.txt")
            .Select(row => row.Select(c => c == '#').ToArray()).ToArray();

        int c = 100;

        while (c-- > 0)
        {
            grid = grid.Select((row, y) => row.Select((on, x) =>
            IsCorner(x, y) || NewState(on, Neighbours(x, y).Count(on => on))).ToArray()).ToArray();
        }

        Print();
        Console.WriteLine($"Part 1: {grid.Sum(row => row.Count(on => on))}");

        bool NewState(bool oldState, int neighborsOn) =>
            oldState ? neighborsOn >= 2 && neighborsOn <= 3 : neighborsOn == 3;

        bool IsCorner(int x, int y) =>
            (x == 0 && y == 0) ||
            (x == 0 && y == grid.Length - 1) ||
            (x == grid[0].Length - 1 && y == 0) ||
            (x == grid[0].Length - 1 && y == grid.Length - 1);

        IEnumerable<bool> Neighbours(int X, int Y)
        {
            foreach (var (x, y) in from y in Enumerable.Range(Y - 1, 3)
                                   from x in Enumerable.Range(X - 1, 3)
                                   where !(X == x && Y == y)
                                   where x >= 0 && y >= 0 && x < grid[0].Length && y < grid.Length
                                   select (x, y))
            {
                yield return grid[y][x];
            }
        }

        void Print()
        {
            var b = new System.Text.StringBuilder();
            foreach (var (x, y) in from y in Enumerable.Range(0, grid.Length)
                                   from x in Enumerable.Range(0, grid[0].Length + 1)
                                   select (x, y))
            {
                b.Append(x >= grid[0].Length ? '\n' : grid[y][x] ? '#' : '.');
            }
            Console.WriteLine(b.ToString());
        }
    }
}