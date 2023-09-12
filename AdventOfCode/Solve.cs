namespace AdventOfCode;

using System.Diagnostics;
public static class Problem
{
    public static void Solve()
    {
        long start = Stopwatch.GetTimestamp(), c = 0, runs = 1;

        while (c++ < runs)
        {
            //AdventOfCode2016.Day20.Solve();
            //AdventOfCode2015.Day17.Solve();
            //AdventOfCode2016.Day24.Solve();
            AdventOfCode2017.Day7.Solve();
        }

        Console.WriteLine($"Duration: {Stopwatch.GetElapsedTime(start).Divide(runs).TotalMilliseconds:F1} ms");
    }
}