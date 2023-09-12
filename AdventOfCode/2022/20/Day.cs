namespace AdventOfCode2022;

public static class Day20
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/20/Input.txt")
            .Select(int.Parse).ToList();

        var data = input.ToList();

        for (int i = 0; i < input.Count; i++)
        {
            //Console.WriteLine(string.Join(',', data));

            var value = input[i];
            var position = data.IndexOf(value);

            data.RemoveAt(position);
            data.Insert(mod(position + value, input.Count - 1), value);
        }

        var result1 = Enumerable.Range(1, 3).Sum(i => data[(data.IndexOf(0) + (i * 1000)) % data.Count]);
        var result2 = 0;

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        static int mod(int x, int m) => ((x % m) + m) % m;
    }
}