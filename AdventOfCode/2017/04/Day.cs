namespace AdventOfCode2017
{
    public static class Day4
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2017/04/Input.txt").Select(row => row.Split(' '));

            var result1 = input
                .Count(p => p.Length == p.Distinct().Count());

            var result2 = input.Select(p => p.Select(w => new string(w.Order().ToArray())).ToArray())
                .Count(p => p.Length == p.Distinct().Count());

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}