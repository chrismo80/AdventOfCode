namespace AdventOfCode2015
{
    public static class Day14
    {
        public static void Solve()
        {
            var reindeers = File.ReadAllLines("AdventOfCode/2015/14/Input.txt")
                .Select(row => row.Split(' ').Where(word => int.TryParse(word, out int value)).Select(int.Parse).ToArray())
                .Select(words => (Speed: words[0], Flying: words[1], Resting: words[2]));

            const int duration = 2503;

            var distances = reindeers.Select(reindeer => Enumerable.Range(1, duration)
                .Select(seconds => Distance(seconds, reindeer)));

            var points = Enumerable.Range(1, duration)
                .Select(seconds => reindeers.Select(reindeer => Distance(seconds, reindeer)))
                .Select(seconds => seconds.Select(distance => distance == seconds.Max() ? 1 : 0));

            var result1 = distances.Max(distances => distances.Last());
            var result2 = reindeers.Select((_, i) => points.Sum(reindeer => reindeer.ElementAt(i))).Max();

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

            static int Distance(int seconds, (int Speed, int Flying, int Resting) r) =>
                (seconds / (r.Flying + r.Resting) * r.Speed * r.Flying) +
                (Math.Min(seconds % (r.Flying + r.Resting), r.Flying) * r.Speed);
        }
    }
}