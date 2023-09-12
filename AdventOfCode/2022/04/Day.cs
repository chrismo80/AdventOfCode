namespace AdventOfCode2022;
public static class Day4
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/04/Input.txt")
            .Select(l => l.Split(",")
                .Select(e => e.Split("-").Select(int.Parse))
                .Select(r => Enumerable.Range(r.First(), r.Last() - r.First() + 1))
                .OrderBy(r => r.Count()));

        var result1 = input.Count(e => e.First().Intersect(e.Last()).Count() == e.First().Count());
        var result2 = input.Count(e => e.First().Intersect(e.Last()).Any());

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }
}