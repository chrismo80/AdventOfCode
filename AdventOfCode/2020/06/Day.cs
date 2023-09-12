namespace AdventOfCode2020;

public static class Day6
{
    public static void Solve()
    {
        var input = File.ReadAllText("AdventOfCode/2020/06/Input.txt").Split("\n\n");

        var result1 = input.Sum(g => g.Replace("\n", "").Distinct().Count());
        var result2 = input.Sum(g => g.Split("\n")
            .Aggregate((p1, p2) => new string(p1.Intersect(p2).ToArray())).Length);

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }
}