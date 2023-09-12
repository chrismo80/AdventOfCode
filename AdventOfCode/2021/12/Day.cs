namespace AdventOfCode2021;
public static class Day12
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2021/12/Input.txt").Select(row => row.Split('-'));

        var graph = new PathFinding.Graph<string>();

        input.ToList().ForEach(edge => graph.Add((edge[0], edge[1])));

        bool Part1(IEnumerable<string> path, string current) => current.All(c => char.IsUpper(c));

        bool Part2(IEnumerable<string> path, string current) => current.All(c => char.IsUpper(c) ||
            (path.Where(v => v.All(c => char.IsLower(c))).GroupBy(v => v).Max(g => g.Count()) <= 1 &&
            current != "start"));

        var result1 = graph.AllPaths("start", "end", Part1);
        var result2 = graph.AllPaths("start", "end", Part2);

        Console.WriteLine($"Part 1: {result1.Count()}, Part 2: {result2.Count()}");

        var result0 = graph.BreadthFirstSearch("start", "end").Count;
    }
}