namespace AdventOfCode2017
{
    public static class Day3
    {
        public static void Solve()
        {
            var input = int.Parse(File.ReadAllText("AdventOfCode/2017/03/Input.txt"));

            int result1 = 0, value1 = 1, x = 0, y = 0, corner = 1;
            long result2 = 0, value2 = 1;

            var grid = new Dictionary<(int X, int Y), long>();

            while (result1 == 0)
            {
                for (int i = 0; i < corner; i++)
                {
                    if (value1 == input)
                        result1 = Math.Abs(x) + Math.Abs(y);

                    x += corner % 2 == 0 ? -1 : 1;
                    value1++;
                }

                for (int i = 0; i < corner; i++)
                {
                    if (value1 == input)
                        result1 = Math.Abs(x) + Math.Abs(y);

                    y += corner % 2 == 0 ? 1 : -1;
                    value1++;
                }
                corner++;
            }

            x = 0; y = 0; corner = 1;

            while (result2 == 0)
            {
                for (int i = 0; i < corner; i++)
                {
                    grid[(x, y)] = value2;

                    if (value2 > input)
                    {
                        result2 = value2;
                        break;
                    }

                    x += corner % 2 == 0 ? -1 : 1;

                    value2 = grid.Where(kvp => Math.Abs(kvp.Key.X - x) <= 1 && Math.Abs(kvp.Key.Y - y) <= 1)
                        .Sum(kvp => kvp.Value);
                }

                for (int i = 0; i < corner; i++)
                {
                    grid[(x, y)] = value2;

                    if (value2 > input)
                    {
                        result2 = value2;
                        break;
                    }

                    y += corner % 2 == 0 ? 1 : -1;
                    value2 = grid.Where(kvp => Math.Abs(kvp.Key.X - x) <= 1 && Math.Abs(kvp.Key.Y - y) <= 1)
                        .Sum(kvp => kvp.Value);
                }
                corner++;
            }

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}