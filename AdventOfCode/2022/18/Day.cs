namespace AdventOfCode2022;

public static class Day18
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/18/Test.txt")
            .Select(l => l.Split(',').Select(int.Parse).ToArray())
            .Select(c => (X: c[0], Y: c[1], Z: c[2])).ToArray();

        var result1 = input.Sum(c => 6 - BlockedSides(c, input));
        var result2 = result1 - 0;

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        static int BlockedSides((int X, int Y, int Z) c, (int, int, int)[] all) => new List<(int, int, int)>
            {
                (c.X - 1, c.Y, c.Z), (c.X, c.Y - 1, c.Z), (c.X, c.Y, c.Z - 1),
                (c.X + 1, c.Y, c.Z), (c.X, c.Y + 1, c.Z), (c.X, c.Y, c.Z + 1)
            }.Intersect(all).Count();
    }
}