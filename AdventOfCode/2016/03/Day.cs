namespace AdventOfCode2016
{
    public static class Day3
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2016/03/Input.txt")
                .Select(row => row.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse).ToArray());

            var result2 = input.Select(g => g[0])
                .Concat(input.Select(g => g[1]))
                .Concat(input.Select(g => g[2]))
                .Chunk(3);

            Console.WriteLine($"Part 1: {input.Count(sides => IsTriangle(sides))}");
            Console.WriteLine($"Part 2: {result2.Count(sides => IsTriangle(sides))}");

            static bool IsTriangle(int[] s) => s[0] + s[1] > s[2] && s[0] + s[2] > s[1] && s[1] + s[2] > s[0];
        }
    }
}