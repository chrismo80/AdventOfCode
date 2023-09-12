namespace AdventOfCode2020;
public static class Day2
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/02/Input.txt").Select(line =>
            (
                Left: int.Parse(line.Split(':')[0].Split(' ')[0].Split('-')[0]),
                Right: int.Parse(line.Split(':')[0].Split(' ')[0].Split('-')[1]),
                Char: line.Split(':')[0][^1..][0],
                Password: line.Split(": ")[1]
            )).ToList();

        var result1 = input
            .Select(x => (Specs: x, Count: x.Password.Count(c => c == x.Char)))
            .Count(c => c.Count >= c.Specs.Left && c.Count <= c.Specs.Right);

        var result2 = input
            .Count(p => p.Password[p.Left - 1] == p.Char ^ p.Password[p.Right - 1] == p.Char);

        Console.WriteLine($"Part 1: {result1} (418), Part 2: {result2} (616)");
    }
}