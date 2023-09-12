namespace AdventOfCode2016
{
    public static class Day8
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2016/08/Input.txt");

            var screen = new bool[50, 6];

            foreach (var step in input)
            {
                if (step.StartsWith("rect"))
                    AddRect(step.Split(' ', 'x').Skip(1).Select(int.Parse).ToArray());
                else
                    RotateLine(step.Split(' ', '='), step.Contains("column") ? 1 : 0);
            }

            var result1 = from bool pixel in screen where pixel select pixel;

            Print();

            Console.WriteLine($"Pixels on: {result1.Count()}");

            void AddRect(int[] size)
            {
                foreach (var (x, y) in from x in Enumerable.Range(0, size[0])
                                       from y in Enumerable.Range(0, size[1])
                                       select (x, y))
                {
                    screen[x, y] = true;
                }
            }

            void RotateLine(string[] words, int dimension)
            {
                int line = int.Parse(words[3]);
                int distance = int.Parse(words[5]);

                var pixels = Enumerable.Range(0, screen.GetLength(dimension))
                    .Select(i => screen[dimension == 1 ? line : i, dimension == 1 ? i : line]);

                var newPixels = Rotate(pixels, distance);

                foreach (int i in Enumerable.Range(0, screen.GetLength(dimension)))
                    screen[dimension == 1 ? line : i, dimension == 1 ? i : line] = newPixels[i];
            }

            static List<T> Rotate<T>(IEnumerable<T> list, int distance) =>
                list.TakeLast(distance).Concat(list.SkipLast(distance)).ToList();

            void Print()
            {
                foreach (var (x, y) in from y in Enumerable.Range(0, screen.GetLength(1))
                                       from x in Enumerable.Range(0, screen.GetLength(0) + 1)
                                       select (x, y))
                {
                    Console.Write(x == screen.GetLength(0) ? '\n' : screen[x, y] ? '#' : '.');
                }
            }
        }
    }
}