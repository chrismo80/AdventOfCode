namespace AdventOfCode2022;
public static class Day3
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/03/Input.txt");

        var result1 = input
            .Select(line => line.Chunk(line.Length / 2))
            .Select(pack => pack.First().Intersect(pack.Last()).Single())
            .Sum(item => Char.IsUpper(item) ? (item - 'A' + 27) : (item - 'a' + 1));

        var result2 = input.Chunk(3)
            .Select(group => group[0].Intersect(group[1]).Intersect(group[2]).Single())
            .Sum(item => Char.IsUpper(item) ? (item - 'A' + 27) : (item - 'a' + 1));

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }
}