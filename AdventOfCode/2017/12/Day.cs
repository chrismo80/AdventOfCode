namespace AdventOfCode2017
{
    public static class Day12
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2017/12/Input.txt")
                .Select(row => row.Split(new string[] { "<->", "," }, StringSplitOptions.TrimEntries)
                    .Select(int.Parse));

            var all = input.SelectMany(p => p).ToHashSet();
            var found = new HashSet<int>();

            int count = 0, groups = 0, result1 = 0;

            while (found.Count < all.Count)
            {
                int start = found.Count == 0 ? 0 : all.Except(found).First();

                found.Add(start);

                while (count != found.Count)
                {
                    count = found.Count;

                    foreach (var connections in input.Where(p => !found.Contains(p.First())))
                    {
                        if (found.Intersect(connections).Any())
                            found.UnionWith(connections);
                    }
                }

                groups++;
                result1 = result1 == 0 ? found.Count : result1;
            }

            Console.WriteLine($"Part 1: {result1}, Part 2: {groups}");
        }
    }
}