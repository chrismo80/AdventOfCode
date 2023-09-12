namespace AdventOfCode2018;

public static class Day23
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/23/Input.txt")
            .Select(row => row.Split('<', '>', ',', '='))
            .Select(items =>
                (
                    Pos: items.Skip(2).Take(3).Select(long.Parse).ToArray(),
                    Radius: long.Parse(items.Last())
                ))
            .ToArray();

        var strongest = input.OrderBy(robot => robot.Radius).Last();

        var result1 = input.Count(robot =>
            robot.Pos.Zip(strongest.Pos, (r, s) => Math.Abs(r - s)).Sum() <= strongest.Radius);

        Console.WriteLine($"Part 1: {result1}");

        var bestRobot = input
            .OrderByDescending(robot => RobotsInRange(robot.Pos))
            .ThenBy(robot => robot.Pos.Sum());

        var bestRanges = bestRobot.Select(robot => RobotsInRange(robot.Pos));
        var robotsNearBy = RobotsInRange(bestRobot.First().Pos);

        Console.WriteLine($"Part 2: {0}");

        int RobotsInRange(long[] pos) =>
            input.Count(robot => robot.Pos.Zip(pos, (i, j) => Math.Abs(i - j)).Sum() <= robot.Radius);
    }
}