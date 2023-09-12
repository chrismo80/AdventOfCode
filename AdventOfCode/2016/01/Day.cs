namespace AdventOfCode2016
{
    public static class Day1
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2016/01/Input.txt")[0].Split(", ");

            var (X, Y) = (0, 0);
            var trail = new HashSet<(int, int)>();
            int view = 0;
            var result2 = (X: 0, Y: 0);

            foreach (var move in input)
            {
                view = (view + 4 + (move[0] == 'L' ? -1 : 1)) % 4;

                for (int i = 0; i < int.Parse(new string(move.Skip(1).ToArray())); i++)
                {
                    X = view == 1 ? X + 1 : view == 3 ? X - 1 : X;
                    Y = view == 2 ? Y + 1 : view == 0 ? Y - 1 : Y;

                    if (!trail.Add((X, Y)))
                        result2 = result2 == (0, 0) ? (X, Y) : result2;
                }
            }

            Console.WriteLine($"Part 1: {Math.Abs(X) + Math.Abs(Y)}");
            Console.WriteLine($"Part 2: {Math.Abs(result2.X) + Math.Abs(result2.Y)}");
        }
    }
}