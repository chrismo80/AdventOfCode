namespace AdventOfCode2018;
using Extensions;
public static class Day1
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/01/Input.txt").Select(int.Parse).ToArray();

        int frequency = 0;
        var history = new HashSet<int> { frequency };

        var lastChange = input.RepeatForever().SkipWhile(change => history.Add(frequency += change)).First();

        Console.WriteLine($"Part 1; {input.Sum()}, Part 2: {frequency}");
    }
}