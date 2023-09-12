namespace AdventOfCode2015
{
    using System.Text.RegularExpressions;
    public static class Day16
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/16/Input.txt")
                .Select(line => line.Split(new string[] { ": ", ", " }, StringSplitOptions.TrimEntries));

            var aunt = input.Single(items =>
                Greater(items, "cats", 7) &&
                Greater(items, "trees", 3) &&
                Fewer(items, "pomeranians", 3) &&
                Fewer(items, "goldfish", 5) &&
                Equals(items, "children", 3) &&
                Equals(items, "samoyeds", 2) &&
                Equals(items, "akitas", 0) &&
                Equals(items, "vizslas", 0) &&
                Equals(items, "cars", 2) &&
                Equals(items, "perfumes", 1))[0];

            Console.WriteLine($"{aunt}");

            static bool Equals(string[] items, string name, int count) =>
                !items.Contains(name) || int.Parse(items[items.ToList().IndexOf(name) + 1]) == count;

            static bool Greater(string[] items, string name, int count) =>
                !items.Contains(name) || int.Parse(items[items.ToList().IndexOf(name) + 1]) > count;

            static bool Fewer(string[] items, string name, int count) =>
                !items.Contains(name) || int.Parse(items[items.ToList().IndexOf(name) + 1]) < count;
        }
    }
}