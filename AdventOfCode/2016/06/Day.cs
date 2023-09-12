namespace AdventOfCode2016
{
    public static class Day6
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2016/06/Test.txt");

            var transposed = Enumerable.Range(0, input[0].Length)
                .Select(i => input.Select(word => word[i]));

            var mostCommon = transposed
                .Select(word => word.GroupBy(c => c).OrderBy(g => g.Count()).Select(g => g.Key).Last());

            var leastCommon = transposed
                .Select(word => word.GroupBy(c => c).OrderBy(g => g.Count()).Select(g => g.Key).First());

            Console.WriteLine($"Part 1: {new string(mostCommon.ToArray())}");
            Console.WriteLine($"Part 2: {new string(leastCommon.ToArray())}");
        }
    }
}