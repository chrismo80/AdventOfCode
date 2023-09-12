namespace AdventOfCode2015
{
    using Extensions;
    public static class Day9
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/09/Input.txt").Select(row => row.Split(' '))
                .Select(words => (From: words[0], To: words[2], Distance: int.Parse(words[4]))).ToList();

            var distances = new Dictionary<(string, string), int>();
            input.ForEach(edge => distances[(edge.From, edge.To)] = distances[(edge.To, edge.From)] = edge.Distance);

            var locations = input.Select(d => d.From).Concat(input.Select(d => d.To)).Distinct();
            var routes = locations.Permutations().OrderBy(TotalDistance).ToHashSet();

            Console.WriteLine($"Part 1: {TotalDistance(routes.First())} ({string.Join('-', routes.First())})");
            Console.WriteLine($"Part 2: {TotalDistance(routes.Last())} ({string.Join('-', routes.Last())})");

            int TotalDistance(IEnumerable<string> route) => Enumerable.Range(0, route.Count() - 1)
                .Sum(i => distances[(route.ElementAt(i), route.ElementAt(i + 1))]);
        }
    }
}