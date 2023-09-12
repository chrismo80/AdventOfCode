namespace AdventOfCode2021
{
    public static class Day8
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/08/Test.txt")
                .Select(l => l.Split("|")
                    .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries)));

            var result1 = input
                .Select(l => l.Last().Select(d => d.Length))
                .Sum(x => x.Count(d => d == 2 || d == 3 || d == 4 || d == 7));

            var result2 = 0;

            Console.WriteLine($"Part 1: 26 {result1}, Part 2: 61229 {result2}");
        }
    }
}