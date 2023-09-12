namespace AdventOfCode2017
{
    public static class Day17
    {
        public static void Solve()
        {
            int value = 0, pos = 0, result2 = 0, input = 356;
            var buffer = new List<int>() { 0 };

            while (value++ < 2017)
            {
                pos += input;
                pos %= value;
                buffer.Insert(pos++, value);
            }

            Console.WriteLine($"Part 1: {buffer[pos]}");

            pos = 0; value = 0;

            while (value++ < 50_000_000)
            {
                pos += input;
                pos %= value;

                if (pos++ == 0)
                    result2 = value;
            }

            Console.WriteLine($"Part 2: {result2}");
        }
    }
}