namespace AdventOfCode2017
{
    public static class Day16
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2017/16/Input.txt")[0].Split(',');

            var history = new HashSet<string>();
            int dances = 0, cycle = 0;

            var programs = "abcdefghijklmnop".ToList();

            while (history.Add(new string(programs.ToArray())))
            {
                Dance();
                cycle++;
            }

            programs = "abcdefghijklmnop".ToList();

            while (dances++ < 1_000_000_000 % cycle)
                Dance();

            Console.WriteLine($"Part 2: {new string(programs.ToArray())}");

            void Dance()
            {
                foreach (var move in input)
                {
                    switch (move[0])
                    {
                        case 's':
                            int spins = int.Parse(move[1..]);
                            while (spins-- > 0)
                                programs = programs.Prepend(programs.Last()).SkipLast(1).ToList();
                            break;

                        case 'x':
                            var pos = move[1..].Split('/').Select(int.Parse);
                            Swap(pos.First(), pos.Last());
                            break;

                        case 'p':
                            var items = move[1..].Split('/').Select(char.Parse);
                            Swap(programs.IndexOf(items.First()), programs.IndexOf(items.Last()));
                            break;
                    }
                }
            }

            void Swap(int pos1, int pos2)
            {
                int min = Math.Min(pos1, pos2);
                int max = Math.Max(pos1, pos2);

                char low = programs[min];
                char high = programs[max];

                programs.RemoveAt(min);
                programs.RemoveAt(max - 1);

                programs.Insert(min, high);
                programs.Insert(max, low);
            }
        }
    }
}