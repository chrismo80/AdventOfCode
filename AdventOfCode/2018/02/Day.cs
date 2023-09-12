namespace AdventOfCode2018;

public static class Day2
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/02/Input.txt");

        int Count(int match) => input.Count(box => box.Select(c => box.Count(x => x == c)).Contains(match));

        var result2 = Enumerable.Range(0, input[0].Length)
            .Select(position => input.Select(box => box.Remove(position, 1))
                .GroupBy(box => box).Where(group => group.Count() > 1).Select(group => group.Key)) // find duplicates
            .Single(l => l.Any()).Single();

        Console.WriteLine($"Part 1: {Count(2) * Count(3)}, Part 2: {result2}");
    }
}