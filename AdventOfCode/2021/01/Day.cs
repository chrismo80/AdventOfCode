namespace AdventOfCode2021
{
    public static class Day1
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/01/Input.txt")
                .Select(x => int.Parse(x));

            var result1 = input
                .Aggregate((Value: 0, Counter: 0), (last, current) =>
                (
                    Value: current,
                    Counter: last.Value < current ? last.Counter + 1 : last.Counter
                )).Counter - 1;

            var result2 = Enumerable.Range(0, input.Count() - 2)
                .Select(i => input.Skip(i).Take(3).Sum())
                .Aggregate((Value: 0, Counter: 0), (last, current) =>
                (
                    Value: current,
                    Counter: last.Value < current ? last.Counter + 1 : last.Counter
                )).Counter - 1;

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}