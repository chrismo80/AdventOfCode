namespace AdventOfCode2015;
using Extensions;
public static class Day15
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2015/15/Input.txt")
            .Select(row => row.Split(':', ',', ' ')
                .Where(word => int.TryParse(word, out int value)).Select(int.Parse))
            .ToArray();

        var result1 = new List<long>();
        var result2 = new List<long>();

        foreach (var recipe in from p0 in Enumerable.Range(0, 101)
                               from p1 in Enumerable.Range(0, 101)
                               from p2 in Enumerable.Range(0, 101)
                               where p0 + p1 + p2 <= 100
                               select new int[] { p0, p1, p2, 100 - p0 - p1 - p2 })
        {
            result1.Add(input[0].Take(4).Select(value => value * recipe[0])
                .Zip(input[1].Take(4).Select(value => value * recipe[1]), (x, y) => x + y)
                .Zip(input[2].Take(4).Select(value => value * recipe[2]), (x, y) => x + y)
                .Zip(input[3].Take(4).Select(value => value * recipe[3]), (x, y) => x + y)
                .Product(score => Math.Max(0, score)));

            if (input.Select((p, i) => p.Last() * recipe[i]).Sum() == 500)
                result2.Add(result1.Last());
        }

        Console.WriteLine($"Part 1: {result1.Max()}, Part 2: {result2.Max()}");
    }
}