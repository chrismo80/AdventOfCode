namespace AdventOfCode2015
{
    public static class Day2
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/02/Input.txt")
                .Select(l => (l.Split('x').Select(int.Parse)).Order().ToArray());

            var result1 = input.Sum(b => (3 * b[0] * b[1]) + (2 * b[1] * b[2]) + (2 * b[2] * b[0]));
            var result2 = input.Sum(b => (2 * b[0]) + (2 * b[1]) + (b[0] * b[1] * b[2]));

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}