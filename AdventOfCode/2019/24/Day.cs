namespace AdventOfCode2019;
public static class Day24
{
    public static void Solve()
    {
        var grid = File.ReadAllLines("AdventOfCode/2019/24/Input.txt");

        Console.WriteLine(string.Join('\n', grid));

        var history = new HashSet<string> { string.Join('\n', grid) };

        int minutes = 0, cycleLength = 0, cycleStart = 0, duration = 1000;

        while (minutes++ < duration)
        {
            grid = grid.Select((row, y) => new string(row.Select((acre, x) =>
                NewState(acre, Neighbors(x, y))).ToArray())).ToArray();

            if (cycleLength == 0 && !history.Add(string.Join('\n', grid)))
            {
                cycleStart = history.ToList().IndexOf(string.Join('\n', grid));
                cycleLength = minutes - cycleStart;
                minutes = cycleStart + ((duration - cycleStart) / cycleLength * cycleLength);
                break;
            }
        }

        Console.WriteLine(string.Join('\n', grid));

        var result = grid.SelectMany(tile => tile).Select((Value, Pos) => (Value, Pos))
            .Where(tile => tile.Value == '#').Sum(tile => Math.Pow(2, tile.Pos));

        Console.WriteLine($"Part: {result}");

        char NewState(char tile, IEnumerable<char> neighbors) => tile switch
        {
            '#' => neighbors.Count(n => n == '#') != 1 ? '.' : tile,
            '.' => neighbors.Any(n => n == '#') && neighbors.Count(n => n == '#') <= 2 ? '#' : tile,
            _ => tile,
        };

        IEnumerable<char> Neighbors(int x, int y)
        {
            if (y > 0) yield return grid[y - 1][x];
            if (x > 0) yield return grid[y][x - 1];
            if (y < grid.Length - 1) yield return grid[y + 1][x];
            if (x < grid[y].Length - 1) yield return grid[y][x + 1];
        }
    }
}