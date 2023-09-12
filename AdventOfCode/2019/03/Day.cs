namespace AdventOfCode2019;
public static class Day3
{
    public static void Solve()
    {
        var wires = File.ReadAllLines("AdventOfCode/2019/03/Input.txt")
            .Select(row => CreateWire(row.Split(',')));

        var intersections = wires.First().Intersect(wires.Last());

        var result1 = intersections
            .Select(cross => Math.Abs(cross.X) + Math.Abs(cross.Y))
            .Order().First();

        var result2 = intersections
            .Select(cross => wires.Select(wire => wire.TakeWhile(segment => segment != cross).Count() + 1))
            .Min(distances => distances.Sum());

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        static HashSet<(int X, int Y)> CreateWire(string[] path)
        {
            var wire = new HashSet<(int X, int Y)>();
            var (X, Y) = (0, 0);

            foreach (var dir in path)
            {
                var length = int.Parse(new string(dir.Skip(1).ToArray()));

                switch (dir[0])
                {
                    case 'R':
                        foreach (var segment in Enumerable.Range(X + 1, length))
                            wire.Add((segment, Y));
                        X += length;
                        break;

                    case 'L':
                        foreach (var segment in Enumerable.Range(X - length, length).Reverse())
                            wire.Add((segment, Y));
                        X -= length;
                        break;

                    case 'D':
                        foreach (var segment in Enumerable.Range(Y + 1, length))
                            wire.Add((X, segment));
                        Y += length;
                        break;

                    case 'U':
                        foreach (var segment in Enumerable.Range(Y - length, length).Reverse())
                            wire.Add((X, segment));
                        Y -= length;
                        break;
                }
            }
            return wire;
        }
    }
}