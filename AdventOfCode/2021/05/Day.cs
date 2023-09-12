namespace AdventOfCode2021;
using Extensions;
public static class Day5
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2021/05/Test.txt")
            .Select(l => (
                X1: int.Parse(l.Split("->")[0].Trim().Split(",")[0]),
                Y1: int.Parse(l.Split("->")[0].Trim().Split(",")[1]),
                X2: int.Parse(l.Split("->")[1].Trim().Split(",")[0]),
                Y2: int.Parse(l.Split("->")[1].Trim().Split(",")[1])
                ));

        var result1 = Enumerable.Range(0, 1000)
            .Select(x => Enumerable.Range(0, 1000).Select(y => input
                .Where(p => p.X1 == p.X2 || p.Y1 == p.Y2)
                .Count(x, y)))
            .Sum(row => row.Count(p => p > 1));

        Console.WriteLine($"Part 1: {result1}, Part 2: ???");
    }

    private static int Count(this IEnumerable<(int X1, int Y1, int X2, int Y2)> data, int x, int y) =>
        data.Count(l => x.IsWithin(l.X1, l.X2) && y.IsWithin(l.Y1, l.Y2));
}