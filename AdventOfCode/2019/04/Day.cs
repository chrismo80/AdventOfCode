namespace AdventOfCode2019;
public static class Day4
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2019/04/Input.txt")[0].Split('-').Select(int.Parse);

        var passwords = Enumerable.Range(input.First(), input.Last() - input.First() + 1)
            .Select(n => n.ToString());

        var result1 = passwords
            .Where(p => p.SequenceEqual(p.Order()) && p.GroupBy(c => c).Any(group => group.Count() > 1));

        var result2 = passwords
            .Where(p => p.SequenceEqual(p.Order()) && p.GroupBy(c => c).Any(group => group.Count() == 2));

        Console.WriteLine($"Part 1: {result1.Count()}, Part 2: {result2.Count()}");
    }
}