namespace AdventOfCode2020;
public static class Day11
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/11/Test.txt");

        var seats = (from y in Enumerable.Range(0, input.Length)
                     from x in Enumerable.Range(0, input[0].Length)
                     where input[y][x] == 'L'
                     select (X: x, Y: y))
                    .ToHashSet();

        var result1 = 0; //FinalSeatsOccupied(seats, 1);
        var result2 = FinalSeatsOccupied(seats, 2);

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        static int FinalSeatsOccupied(HashSet<(int X, int Y)> seats, int part)
        {
            var occupied = new HashSet<(int X, int Y)>();
            var toBeSeated = new HashSet<(int X, int Y)>();
            var toBeLeft = new HashSet<(int X, int Y)>();

            do
            {
                PrintMap(seats, occupied);

                toBeSeated.Clear();
                toBeLeft.Clear();

                foreach (var seat in seats.Except(occupied))
                {
                    if (part == 1 && !Neighbours1(seat, occupied).Any())
                        toBeSeated.Add(seat);

                    if (part == 2 && !Neighbours2(seat, occupied).Any())
                        toBeSeated.Add(seat);
                }

                foreach (var seat in occupied)
                {
                    if (part == 1 && Neighbours1(seat, occupied).Count >= 4)
                        toBeLeft.Add(seat);

                    var n = Neighbours2(seat, occupied);

                    if (part == 2 && Neighbours2(seat, occupied).Count >= 5)
                        toBeLeft.Add(seat);
                }

                occupied.UnionWith(toBeSeated);
                occupied.RemoveWhere(seat => toBeLeft.Contains(seat));
            }
            while (toBeSeated.Any() || toBeLeft.Any());

            return occupied.Count;
        }

        static HashSet<(int X, int Y)> Neighbours1((int X, int Y) seat, HashSet<(int X, int Y)> occupied) =>
            occupied.Where(n => Math.Abs(n.X - seat.X) < 2 && Math.Abs(n.Y - seat.Y) < 2 && n != seat).ToHashSet();

        static HashSet<(int X, int Y)> Neighbours2((int X, int Y) seat, HashSet<(int X, int Y)> occupied) =>
            occupied.Where(n =>
                (   // First of all 8 directions only, missing implementation
                    (Math.Abs(n.X - seat.X) == Math.Abs(n.Y - seat.Y)) ||
                    (n.X == seat.X) ||
                    (n.Y == seat.Y)
                ) && n != seat).ToHashSet();
    }

    static void PrintMap(HashSet<(int X, int Y)> seats, HashSet<(int X, int Y)> occupied)
    {
        Thread.Sleep(100);
        var output = new List<string>();

        for (int y = seats.Min(w => w.Y); y <= seats.Max(w => w.Y); y++)
        {
            var row = new List<char>();

            for (int x = seats.Min(w => w.X); x <= seats.Max(w => w.X); x++)
                row.Add(occupied.Contains((x, y)) ? '#' : seats.Contains((x, y)) ? 'L' : '.');

            output.Add(new string(row.ToArray()));
        }

        File.WriteAllLines("AdventOfCode/2020/11/Output.txt", output);
    }
}