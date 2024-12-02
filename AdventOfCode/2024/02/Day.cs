using System.Security.Cryptography;

namespace AdventOfCode2024;
using Extensions;
public static class Day2
{
    public static void Solve()
    {
        var reports = File.ReadAllLines("AdventOfCode/2024/02/Input.txt")
            .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
            .ToList();

        var safeReports = reports.Where(l => Safe(Diffs(l)));
        
        var nearlySafeReports = reports.Except(safeReports)
            .Where(l => SubLevels(l).Select(Diffs).Any(Safe));
        
        Console.WriteLine($"Part 1: {safeReports.Count()}, Part 2: {safeReports.Count() + nearlySafeReports.Count()}");
    }

    private static IEnumerable<int> Diffs(IEnumerable<int> levels) =>
        levels.Skip(1).Zip(levels.SkipLast(1), (x, y) => x - y);
    
    private static bool Safe(IEnumerable<int> diffs) =>
        diffs.All(x => x >= 1 && x <= 3) || diffs.All(x => x <= -1 && x >= -3);

    private static IEnumerable<IEnumerable<int>> SubLevels(IEnumerable<int> levels) =>
        Enumerable.Range(0, levels.Count()).Select(i => levels.Where((_, index) => index != i));
}