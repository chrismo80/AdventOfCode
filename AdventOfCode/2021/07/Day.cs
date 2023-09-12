namespace AdventOfCode2021
{
    public static class Day7
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/07/Input.txt")
                [0].Split(",").Select(int.Parse).Order();

            var result1 = Enumerable.Range(input.First(), input.Last() - input.First() + 1)
                .Min(t => input.Sum(x => Math.Abs(t - x)));
            var target1 = Enumerable.Range(input.First(), input.Last() - input.First() + 1)
                .First(t => input.Sum(x => Math.Abs(t - x)) == result1);
            Console.WriteLine($"Part 1: {result1} for target {target1}");

            var result2 = Enumerable.Range(input.First(), input.Last() - input.First() + 1)
                .Min(t => input.Sum(x => Enumerable.Range(1, Math.Abs(t - x)).Sum()));
            var target2 = Enumerable.Range(input.First(), input.Last() - input.First() + 1)
                .First(t => input.Sum(x => Enumerable.Range(1, Math.Abs(t - x)).Sum()) == result2);
            Console.WriteLine($"Part 2: {result2} for target {target2}");
        }
    }
}