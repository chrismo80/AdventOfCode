namespace AdventOfCode2018;
public static class Day10
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/10/Input.txt")
            .Select(row => row.Split(',', '<', '>')
                .Where(_ => int.TryParse(_, out int v)).Select(int.Parse).ToArray())
            .Select(p => (startX: p[0], startY: p[1], deltaX: p[2], deltaY: p[3])).ToArray();

        var grid = input.Select(p => (X: p.startX, Y: p.startY)).ToArray();
        var velos = input.Select(p => (X: p.deltaX, Y: p.deltaY)).ToArray();

        int seconds = 0;

        while (grid.Max(p => p.Y) - grid.Min(p => p.Y) > 10)
        {
            for (int i = 0; i < velos.Length; i++)
            {
                grid[i].X += velos[i].X;
                grid[i].Y += velos[i].Y;
            }
            seconds++;
        }

        Print();

        Console.WriteLine($"Wait time: {seconds}");

        void Print()
        {
            foreach (var pos in from y in Enumerable.Range(grid.Min(p => p.Y), grid.Max(p => p.Y) - grid.Min(p => p.Y) + 1)
                                from x in Enumerable.Range(grid.Min(p => p.X), grid.Max(p => p.X) - grid.Min(p => p.X) + 2)
                                select (x, y))
            {
                Console.Write(pos.x > grid.Max(p => p.X) ? '\n' : grid.Contains(pos) ? '#' : '.');
            }
            Console.WriteLine();
        }
    }
}