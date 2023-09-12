namespace AdventOfCode2021;
using Extensions;
public static class Day9
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2021/09/Input.txt")
            .Select(l => l.Select(c => c - '0').ToArray()).ToArray();

        var sinks = from y in Enumerable.Range(0, input.Length)
                    from x in Enumerable.Range(0, input[0].Length)
                    where Neighbors(x, y).Select(n => input[n.Y][n.X]).All(n => n > input[y][x])
                    select (X: x, Y: y);

        var result1 = sinks.Sum(sink => input[sink.Y][sink.X]) + sinks.Count();
        var result2 = sinks.Select(BaisinSize).Order().TakeLast(3).Product();

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        IEnumerable<(int X, int Y)> Neighbors(int x, int y)
        {
            if (y > 0) yield return (x, y - 1);
            if (x > 0) yield return (x - 1, y);
            if (y < input.Length - 1) yield return (x, y + 1);
            if (x < input[0].Length - 1) yield return (x + 1, y);
        }

        int BaisinSize((int X, int Y) start) // BreadthFirstSearch
        {
            var visited = new HashSet<(int, int)> { start };
            var active = new Queue<(int, int)>();

            active.Enqueue(start);

            while (active.TryDequeue(out (int X, int Y) current))
            {
                foreach (var neighbor in Neighbors(current.X, current.Y)
                    .Where(neighbor => input[neighbor.Y][neighbor.X] < 9 && visited.Add(neighbor)))
                {
                    active.Enqueue(neighbor);
                }
            }

            return visited.Count;
        }
    }
}