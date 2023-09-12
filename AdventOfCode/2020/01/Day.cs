namespace AdventOfCode2020;
public static class Day1
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/01/Input.txt").Select(int.Parse);

        var result1 = (from x in input
                       from y in input
                       where x + y == 2020
                       select x * y).First();

        var result2 = (from x in input
                       from y in input
                       from z in input
                       where x + y + z == 2020
                       select x * y * z).First();

        Console.WriteLine($"Part 1: {result1} (437931), Part 2: {result2} (157667328)");
    }
}