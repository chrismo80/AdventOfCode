namespace AdventOfCode2017;
public static class Day22
{
    public static void Solve()
    {
        var map = File.ReadAllLines("AdventOfCode/2017/22/Input.txt")
            .Select(row => row.ToArray()).ToArray();

        var infected = (from row in Enumerable.Range(0, map.Length)
                        from col in Enumerable.Range(0, map[0].Length)
                        where map[row][col] == '#'
                        select (col, row)).ToHashSet();

        HashSet<(int, int)> weakened = new(), flagged = new();

        var (x, y) = (map[0].Length / 2, map.Length / 2);
        int steps = 0, result = 0, face = 0; // 0 north, 1 east, 2 south, 3 west

        while (steps++ < 10000000)
        {
            if (infected.Contains((x, y)))
            {
                infected.Remove((x, y));
                flagged.Add((x, y));
                face++;
            }
            else if (weakened.Contains((x, y)))
            {
                weakened.Remove((x, y));
                infected.Add((x, y));
                result++;
            }
            else if (flagged.Contains((x, y)))
            {
                flagged.Remove((x, y));
                face += 2;
            }
            else
            {
                weakened.Add((x, y));
                face--;
            }

            face += 4;
            face %= 4;

            x = face == 1 ? x + 1 : face == 3 ? x - 1 : x;
            y = face == 0 ? y - 1 : face == 2 ? y + 1 : y;
        }

        Console.WriteLine($"Part 2: {result}");
    }

}