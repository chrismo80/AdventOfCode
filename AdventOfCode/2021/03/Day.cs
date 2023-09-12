namespace AdventOfCode2021
{
    public static class Day3
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/03/Input.txt");

            var data = input
                .SelectMany(inner => inner.Select((item, index) => (item, index)))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => g.Count(c => c == '1') > input.Length / 2 ? '1' : '0');

            var gamma = Convert.ToInt32(string.Concat(data), 2);
            var epsilon = ~gamma & 0xFFF;

            var result1 = gamma * epsilon;

            var result2 = input
                .SelectMany(inner => inner.Select((item, index) => (item, index)))
                .GroupBy(i => i.index, i => i.item)
                .First();

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}