namespace AdventOfCode2017
{
    public static class Day24
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2017/24/Input.txt")
                .Select(row => row.Split('/').Select(int.Parse))
                .Select(port => (port.First(), port.Last()));

            var bridges = Bridges(input, new List<(int, int)>() { (0, 0) }).ToList();

            var maxLength = bridges.Max(bridge => bridge.Count());

            var result1 = bridges.Max(bridge => bridge.Strength());
            var result2 = bridges.Where(bridge => bridge.Count() == maxLength)
                .Max(bridge => bridge.Strength());

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }

        private static int Strength(this IEnumerable<(int, int)> bridge) =>
            bridge.Sum(port => port.Item1 + port.Item2);

        private static int Match(this (int, int) last, (int, int) next) =>
            last.Item2 == next.Item1 ? 1 : last.Item2 == next.Item2 ? 2 : 0;

        private static IEnumerable<IEnumerable<(int, int)>> Bridges(IEnumerable<(int, int)> unused, IEnumerable<(int, int)> used)
        {
            foreach (var port in unused.Where(port => used.Last().Match(port) > 0))
            {
                var aligned = used.Last().Match(port) == 1 ? port : (port.Item2, port.Item1);

                yield return used.Append(aligned);

                unused = unused.Where(a => !a.Equals(port)).ToList();

                foreach (var segment in Bridges(unused, used.Append(aligned).ToList()))
                    yield return segment;
            }
        }
    }
}