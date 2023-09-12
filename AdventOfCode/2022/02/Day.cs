namespace AdventOfCode2022;
public static class Day2
{
    public static void Solve()
    {
        var data = File.ReadAllLines("AdventOfCode/2022/02/Test.txt")
            .Select(line => (Opp: line[0] - 'A', You: line[2] - 'X'));

        var result1 = data
            .Sum(x => ((4 + x.You - x.Opp) % 3 * 3) + x.You + 1);

        var result2 = data.Select(x => (x.Opp, You: (x.You + 2 + x.Opp) % 3))
            .Sum(x => ((4 + x.You - x.Opp) % 3 * 3) + x.You + 1);

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }
}