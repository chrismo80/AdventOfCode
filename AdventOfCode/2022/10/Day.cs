namespace AdventOfCode2022;
public static class Day10
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/10/Input.txt")
            .Select(line => (
                Cycles: line.StartsWith("add") ? 2 : line == "noop" ? 1 : 0,
                Change: line.StartsWith("add") ? int.Parse(line.Split(' ')[1]) : 0))
            .Select(command => Enumerable.Range(0, command.Cycles)
                .Select(cycle => cycle > 0 ? command.Change : 0))
            .SelectMany(x => x).ToArray();

        var x = new List<int> { 1 };

        foreach (var change in input)
            x.Add(x.Last() + change);

        int SignalStrength(int cycles) => x[cycles - 1] * cycles;

        var result1 = Enumerable.Range(0, 6).Sum(c => SignalStrength((c * 40) + 20));

        Console.WriteLine($"Part 1: {result1} (12740), Part 2: RBPARAGF");

        for (int i = 0; i < x.Count; i++)
            Console.Write((i % 40 == 0 ? "\r\n" : "") + (Math.Abs((i % 40) - x[i]) <= 1 ? "⬜" : "⬛"));
    }
}