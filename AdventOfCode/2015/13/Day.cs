namespace AdventOfCode2015
{
    using Extensions;
    public static class Day13
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/13/Input.txt")
                .Select(row => row.Split(' '))
                .Select(words => (Name: words[0][0], Neighbor: words[10][0],
                    Happiness: int.Parse(words[3]) * (words[2] == "gain" ? 1 : -1)))
                .ToList();

            var result1 = string.Concat(input.Select(p => p.Name).Distinct()).Where(c => c != 'A')
                .Permutations().Select(p => string.Concat(p) + 'A').Max(v => v.Sum(p => Happiness(p, v)));

            var result2 = string.Concat(input.Select(p => p.Name).Distinct())
                .Permutations().Select(p => string.Concat(p) + 'X').Max(v => v.Sum(p => Happiness(p, v)));

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

            int Happiness(char name, IEnumerable<char> permutation) => input
                .Where(p => p.Name == name && permutation.Neighbors(name).Contains(p.Neighbor))
                .Sum(p => p.Happiness);
        }

        static IEnumerable<char> Neighbors(this IEnumerable<char> source, char element)
        {
            int pos = source.ToList().IndexOf(element);

            yield return source.ElementAt((pos + 1) % source.Count());
            yield return source.ElementAt((pos - 1 + source.Count()) % source.Count());
        }
    }
}