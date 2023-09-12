namespace AdventOfCode2018;
public static class Day7
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/07/Input.txt").Select(row => row.Split(' '))
            .Select(words => (Step: words[1][0], Before: words[7][0]));

        var specs = input.Select(i => i.Before).Distinct()
            .Select(b => (Step: b, Required: input.Where(i => i.Before == b).Select(i => i.Step)));

        var done = new List<char>();
        var available = input.Select(x => x.Step).Except(specs.Select(specs => specs.Step)).Order().ToList();

        const int workers = 2;
        int duration = 0;

        while (available.Any())
        {
            done.Add(available[0]);

            duration += 60 + (done.Last() - '@');

            available.AddRange(specs.Where(spec => spec.Required.All(required => done.Contains(required)))
            .Select(spec => spec.Step));
            available = available.Except(done).Distinct().Order().ToList();
        }

        Console.WriteLine($"Part 1: {new string(done.ToArray())}, Part 2: {duration}");
    }
}