namespace AdventOfCode2016
{
    public static class Day15
    {
        public static void Solve()
        {
            var discs = File.ReadAllLines("AdventOfCode/2016/15/Input.txt")
                .Select(row => row.Split(' ', '#', '.'))
                .Select(data => (
                    Number: int.Parse(data[2]),
                    Positions: int.Parse(data[4]),
                    Start: int.Parse(data[12])
                )).ToList();

            int FindWaitTime() => Enumerable.Range(0, int.MaxValue).First(delay =>
                discs.All(disc => (disc.Number + disc.Start + delay) % disc.Positions == 0));

            var result1 = FindWaitTime();

            discs.Add((discs.Max(disc => disc.Number + 1), 11, 0));

            var result2 = FindWaitTime();

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}