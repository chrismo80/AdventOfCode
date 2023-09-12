namespace AdventOfCode2021
{
    public static class Day11
    {
        public static void Solve()
        {
            var data = File.ReadAllLines("AdventOfCode/2021/11/Inputs.txt")
                .Select(l => l.Chunk(1).Select(c => int.Parse(c)).ToArray()).ToArray();

            int result1 = 0, result2 = 0;

            for (int step = 1; step <= 1000; step++)
            {
                data = data.Select(row => row.Select(x => x + 1).ToArray()).ToArray();
                while (data.Any(row => row.Any(val => val >= 10)))
                {
                    foreach (var loc in from x in Enumerable.Range(0, 10)
                                        from y in Enumerable.Range(0, 10)
                                        where data[x][y] >= 10
                                        select (x, y))
                    {
                        data = data.Select((row, x) => row.Select((val, y) =>
                            loc.x == x && loc.y == y ? 0 :
                            Math.Abs(loc.x - x) <= 1 && Math.Abs(loc.y - y) <= 1 && val > 0 ? val + 1 :
                            val).ToArray()).ToArray();
                        result1 += step <= 100 ? 1 : 0;
                    }
                    result2 = result2 == 0 && data.All(row => row.All(val => val == 0)) ? step : result2;
                }
            }
            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}