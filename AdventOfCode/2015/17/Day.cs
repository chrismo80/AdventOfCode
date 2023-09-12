namespace AdventOfCode2015;
using Extensions;
public static class Day17
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2015/17/Input.txt").Select(int.Parse).ToList();

        const int volume = 150;

        var combinations = input.Combinations().Where(c => c.Sum() == volume).ToArray();

        int minContainer = combinations.Min(c => c.Count());

        int result1 = combinations.Length;
        int result2 = combinations.Count(c => c.Count() == minContainer);

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }

    static IEnumerable<IEnumerable<T>> Combinations2<T>(this IEnumerable<T> source) =>
        Enumerable.Range(1, 1 << source.Count()) // 2^input.Count
            .Select(bitMask => source.Where((_, index) => (bitMask & (1 << index)) != 0));

    static IEnumerable<IEnumerable<T>> CombinationsBitMask<T>(this IEnumerable<T> source)
    {
        var count = (int)Math.Pow(2, source.Count());

        for (int combination = 1; combination < count; combination++)
            yield return ListOf(combination);

        IEnumerable<T> ListOf(int bitMask)
        {
            for (int i = source.Count() - 1, bit = count / 2; i >= 0; i--, bit /= 2)
                if ((bitMask & bit) != 0) yield return source.ElementAt(i);
        }
    }

    static IEnumerable<IEnumerable<T>> CombinationsString<T>(this IEnumerable<T> source)
    {
        var count = Math.Pow(2, source.Count());

        for (int comb = 1; comb < count; comb++)
            yield return CombinationString(Convert.ToString(comb, 2).PadLeft(source.Count(), '0'));

        IEnumerable<T> CombinationString(string bitMask)
        {
            for (int pos = 0; pos < bitMask.Length; pos++)
                if (bitMask[pos] == '1') yield return source.ElementAt(pos);
        }
    }

    static IEnumerable<IEnumerable<T>> WithoutRecursion<T>(this IEnumerable<T> source)
    {
        for (int counter = 0; counter < (1 << source.Count()); ++counter)
        {
            var combination = new List<T>();

            for (int i = 0; i < source.Count(); ++i)
                if ((counter & (1 << i)) == 0) combination.Add(source.ElementAt(i));

            yield return combination;
        }
    }

    static IEnumerable<IEnumerable<T>> WithRecursion<T>(this IEnumerable<T> source)
    {
        for (var i = 0; i < (1 << source.Count()); i++)
            yield return Positions(i).Select(n => source.ElementAt(n));

        static IEnumerable<int> Positions(int i)
        {
            for (int n = 0; i > 0; n++)
            {
                if ((i & 1) == 1) yield return n;
                i /= 2;
            }
        }
    }
}