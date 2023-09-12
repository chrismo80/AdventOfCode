namespace AdventOfCode2018;
public static class Day4
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/04/Input.txt").Order();

        var guards = new Dictionary<int, int[]>();
        int guard = 0, start = 0, end;

        foreach (var line in input)
        {
            if (line.Contains("Guard"))
                guard = int.Parse(line.Split('#', ' ')[4]);

            if (!guards.ContainsKey(guard))
                guards[guard] = Enumerable.Repeat(0, 60).ToArray();

            if (line.Contains("falls asleep"))
                start = int.Parse(line.Split(':', ']')[1]);

            if (line.Contains("wakes up"))
            {
                end = int.Parse(line.Split(':', ']')[1]);

                foreach (int minute in Enumerable.Range(start, end - start))
                    guards[guard][minute]++;
            }
        }

        var guard1 = guards.Select(g => (Id: g.Key, Sleep: g.Value.Sum())).OrderBy(g => g.Sleep).Last().Id;
        var guard2 = guards.Select(g => (Id: g.Key, Sleep: g.Value.Max())).OrderBy(g => g.Sleep).Last().Id;

        Console.WriteLine($"Part 1; {guard1 * BestMinute(guard1)}, Part 2: {guard2 * BestMinute(guard2)}");

        int BestMinute(int guard) => guards.First(g => g.Key == guard).Value.ToList()
            .IndexOf(guards.First(g => g.Key == guard).Value.Max());
    }
}