namespace AdventOfCode2021
{
    public static class Day14
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/14/Test.txt");

            var polymer = input[0];

            var insertions = input.Where(line => line.Contains("->"))
                .Select(row => row.Split(" -> ")).ToArray()
                .Select(i => (From: i[0], To: $"{i[0][0]}{i[1]}{i[0][1]}"));

            var elements = polymer.Distinct().Order().Select(e => (E: e, C: polymer.Count(p => p == e)));

            for (int i = 1; i <= 20; i++)
            {
                var pairs = Enumerable.Range(0, polymer.Length - 1)
                    .Select(i => new string(polymer.Skip(i).Take(2).ToArray()))
                    .Select(pair => insertions.Select(i => i.From).Contains(pair) ?
                        insertions.First(i => i.From == pair).To : pair)
                    .Select(pair => new string(pair.Skip(1).ToArray()));

                polymer = $"{polymer[0]}{string.Concat(pairs)}";

                elements = polymer.Distinct().Order().Select(e => (E: e, C: polymer.Count(p => p == e)));

                Console.WriteLine(string.Join(',', elements));
            }
        }
    }
}