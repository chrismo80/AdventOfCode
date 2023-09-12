namespace AdventOfCode2018;
public static class Day5
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2018/05/Input.txt")[0];

        var result2 = Enumerable.Range('a', 26).Select(c => $"{(char)c}")
            .Select(unit => React(input.Replace(unit, "").Replace(unit.ToUpper(), "")));

        Console.WriteLine($"Part 1; {React(input)}, Part 2: {result2.Min()}");

        static int React(string polymer)
        {
            var reactions =
                Enumerable.Range('a', 26).Zip(Enumerable.Range('A', 26),
                    (l, u) => $"{(char)l}{(char)u}").Concat(
                Enumerable.Range('A', 26).Zip(Enumerable.Range('a', 26),
                    (u, l) => $"{(char)u}{(char)l}"));

            int length = 0;

            while (length != polymer.Length)
            {
                length = polymer.Length;

                foreach (var reaction in reactions)
                    polymer = polymer.Replace(reaction, "");
            }

            return polymer.Length;
        }
    }
}