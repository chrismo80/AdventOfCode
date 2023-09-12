namespace AdventOfCode2016
{
    public static class Day19
    {
        public static void Solve()
        {
            var count = int.Parse(File.ReadAllLines("AdventOfCode/2016/19/Input.txt")[0]);

            int player1 = 1, diff = 1, player2 = 1, count2 = count;

            while (count > 1)
            {
                diff *= 2;

                if (count % 2 != 0)
                    player1 += diff;

                count /= 2;
            }

            Console.WriteLine($"Part 1: {player1}, Part 2: {0}");
        }
    }
}