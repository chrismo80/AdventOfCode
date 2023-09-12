namespace AdventOfCode2016;
using Extensions;

public static class Day10
{
    record Robot(int Id, List<int> Chips, List<List<string>> Targets);
    record Output(int Id, List<int> Chips);

    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2016/10/Input.txt")
            .Select(row => row.Split(' ').ToArray());

        var robots = new List<Robot>();
        var outputs = new List<Output>();

        foreach (var words in input.Where(words => words[0] == "bot"))
        {
            robots.Add(new Robot(int.Parse(words[1]),
                new List<int>(),
                new List<List<string>>() { words.Skip(5).Take(2).ToList(), words.TakeLast(2).ToList() }));
        }

        foreach (var words in input.Where(words => words[0] == "value"))
            robots.First(r => r.Id == int.Parse(words[5])).Chips.Add(int.Parse(words[1]));

        var result1 = 0;
        var robot = robots.Single(r => r.Chips.Count > 1);

        while (robot != null)
        {
            var chips = robot.Chips.Order();

            if (chips.First() == 17 && chips.Last() == 61)
                result1 = robot.Id;

            foreach (var (chip, i) in chips.Select((c, i) => (c, i)))
            {
                if (robot.Targets[i][0] == "bot")
                    robots.First(r => r.Id == int.Parse(robot.Targets[i][1])).Chips.Add(chip);

                if (robot.Targets[i][0] == "output")
                {
                    var output = outputs.Find(o => o.Id == int.Parse(robot.Targets[i][1]));

                    if (output == null)
                        outputs.Add(new Output(int.Parse(robot.Targets[i][1]), new List<int>()));

                    outputs.Single(o => o.Id == int.Parse(robot.Targets[i][1])).Chips.Add(chip);
                }
            }

            robot.Chips.Clear();
            robot = robots.Find(r => r.Chips.Count > 1);
        }

        var result2 = outputs.OrderBy(o => o.Id).Take(3).Select(o => o.Chips)
            .SelectMany(c => c).Product();

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }
}