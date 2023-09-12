namespace AdventOfCode2017
{
    public static class Day1
    {
        public static void Solve()
        {
            var input = File.ReadAllText("AdventOfCode/2017/01/Input.txt");

            var result1 = FindMatches(input.Concat(input), 1);
            var result2 = FindMatches(input.Concat(input), input.Length / 2);

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

            static int FindMatches(IEnumerable<char> stream, int distance) =>
                Enumerable.Range(0, stream.Count() / 2)
                    .Where(i => stream.Skip(i).First() == stream.Skip(i + distance).First())
                    .Sum(i => stream.ElementAt(i) - '0');
        }
    }
}