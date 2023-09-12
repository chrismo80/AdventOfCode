namespace AdventOfCode2020;
public static class Day9
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/09/Input.txt").Select(long.Parse);

        var result1 = FindInvalid(input, 25);
        var result2 = SetMatch(input.Skip(FindSet(input, result1)).ToArray(), result1);

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        long FindInvalid(IEnumerable<long> stream, int preamble) => input.ElementAt
            (Enumerable.Range(preamble, stream.Count())
                .First(i => !IsValid(stream.Skip(i - preamble).Take(preamble), stream.ElementAt(i))));

        bool IsValid(IEnumerable<long> preamble, long number) =>
            (from x in preamble from y in preamble where x + y == number select (x, y)).Any();

        int FindSet(IEnumerable<long> stream, long expectedSum) => Enumerable.Range(0, stream.Count())
            .First(i => SetMatch(stream.Skip(i).ToArray(), expectedSum) > 0);

        long SetMatch(long[] stream, long expectedSum)
        {
            long sum = 0;

            for (int i = 0; i < stream.Length; i++)
            {
                sum += stream[i];

                if (sum == expectedSum)
                    return stream.Take(i + 1).Min() + stream.Take(i + 1).Max();
            }
            return 0;
        }
    }
}