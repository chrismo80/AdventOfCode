namespace AdventOfCode2020;
public static class Day15
{
    public static void Solve()
    {
        var spoken = File.ReadAllLines("AdventOfCode/2020/15/Test.txt")[0]
            .Split(',').Select(int.Parse).ToList();

        while (spoken.Count < 2020)
        {
            spoken.Add(spoken.Count(s => s == spoken.Last()) == 1 ? 0 :
                spoken.Count - spoken.LastIndexOf(spoken.Last(), spoken.Count - 2) - 1);
        }

        Console.WriteLine($"Part 1: {spoken.Last()}, Part 2: {0}");
    }
}