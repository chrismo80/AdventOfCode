namespace AdventOfCode2020;

public static class Day7
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/07/Input.txt")
            .Select(text => text.Split(" bag").Where(t => !t.Contains("no other")).SkipLast(1)
                .Select(words => string.Join(' ', words.Split(' ').TakeLast(3))).ToArray())
            .Select(bags => (Outer: bags[0], Inner: bags.Skip(1).ToList())).ToList();

        Console.WriteLine($"Part 1: {CountOuterBags("shiny gold", input)}");
        Console.WriteLine($"Part 2: {CountInnerBags("shiny gold", input) - 1}");

        static int CountOuterBags(string bagColor, List<(string Outer, List<string> Inner)> input)
        {
            var outerBags = new HashSet<string>();
            int counter = -1;

            while (counter != outerBags.Count)
            {
                counter = outerBags.Count;

                foreach (var (Outer, Inner) in input.Where(bag => !outerBags.Contains(bag.Outer)))
                {
                    var innerBags = Inner.Select(words => string.Join(' ', words.Split(' ').Skip(1)));

                    if (innerBags.Contains(bagColor) || innerBags.Intersect(outerBags).Any())
                        outerBags.Add(Outer);
                }
            }

            return outerBags.Count;
        }

        static int CountInnerBags(string bagColor, List<(string Outer, List<string> Inner)> input)
        {
            int counter = 1;

            foreach (var bag in input.First(bag => bag.Outer == bagColor).Inner)
            {
                var color = string.Join(' ', bag.Split(' ').Skip(1));
                counter += int.Parse(bag.Split(' ')[0]) * CountInnerBags(color, input);
            }

            return counter;
        }

        static int CountInnerBagsLINQ(string bagColor, List<(string Outer, List<string> Inner)> input) =>
            input.First(b => b.Outer == bagColor).Inner.Sum
                (bag =>
                    int.Parse(bag.Split(' ')[0]) *
                    CountInnerBagsLINQ(string.Join(' ', bag.Split(' ').Skip(1)), input)
                ) + 1;
    }
}