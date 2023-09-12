namespace AdventOfCode2021
{
    public static class Day13
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/13/Input.txt");

            var paper = input.TakeWhile(line => line != "")
                .Select(row => row.Split(',').Select(int.Parse).ToArray())
                .Select(pos => (X: pos[0], Y: pos[1]))
                .ToHashSet();

            var instructions = input.Where(line => line.StartsWith("fold"))
                .Select(row => row.Split(' ')[2].Split('='))
                .Select(line => (Axis: line[0][0], Line: int.Parse(line[1])));

            foreach (var (Axis, Line) in instructions)
                paper = paper.Fold(Axis, Line);

            paper.Print();

            Console.WriteLine($"Part 1: {paper.Count}");
        }

        static HashSet<(int, int)> Fold(this HashSet<(int X, int Y)> paper, char axis, int line) =>
            paper.Select(p =>
                (
                    axis == 'y' || p.X < line ? p.X : ((p.X - line) * (-1)) + line,
                    axis == 'x' || p.Y < line ? p.Y : ((p.Y - line) * (-1)) + line
                )).ToHashSet();

        static void Print(this HashSet<(int X, int Y)> map)
        {
            var output = new List<string>();

            for (int y = map.Min(d => d.Y); y <= map.Max(d => d.Y); y++)
            {
                var row = new List<char>();

                for (int x = map.Min(d => d.X); x <= map.Max(d => d.X); x++)
                    row.Add(map.Contains((x, y)) ? '#' : '.');

                output.Add(new string(row.ToArray()));
            }

            File.WriteAllLines("AdventOfCode/2021/13/Output.txt", output);
        }
    }
}