namespace AdventOfCode2015
{
    using System.Text.RegularExpressions;
    public static class Day6
    {
        public static void Solve()
        {
            var reg = new Regex("(.*) (\\d+),(\\d+) through (\\d+),(\\d+)");

            var input = File.ReadAllLines("AdventOfCode/2015/06/Input.txt").Select(line => (
                Action: reg.Match(line).Groups[1].Value,
                X1: int.Parse(reg.Match(line).Groups[2].Value),
                Y1: int.Parse(reg.Match(line).Groups[3].Value),
                X2: int.Parse(reg.Match(line).Groups[4].Value),
                Y2: int.Parse(reg.Match(line).Groups[5].Value)
                ));

            var grid1 = Enumerable.Range(0, 1000)
                .Select(_ => Enumerable.Range(0, 1000).Select(_ => 0).ToArray())
                .ToArray();

            var grid2 = Enumerable.Range(0, 1000)
                .Select(_ => Enumerable.Range(0, 1000).Select(_ => 0).ToArray())
                .ToArray();

            foreach (var cmd in input)
            {
                for (int x = cmd.X1; x <= cmd.X2; x++)
                {
                    for (int y = cmd.Y1; y <= cmd.Y2; y++)
                    {
                        grid1[x][y] = cmd.Action == "toggle" ? 1 - grid1[x][y] : cmd.Action == "turn on" ? 1 : 0;
                        grid2[x][y] += cmd.Action == "toggle" ? 2 : cmd.Action == "turn on" ? 1 : -1;
                        grid2[x][y] = Math.Max(grid2[x][y], 0);
                    }
                }
            }

            var result1 = grid1.SelectMany(x => x).Count(x => x > 0);
            var result2 = grid2.SelectMany(x => x).Sum(x => x);

            Console.WriteLine($"Part 1: {result1} (543903), Part 2: {result2} (14687245)");
        }
    }
}