namespace AdventOfCode2019;
public static class Day6
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2019/06/Input.txt")
            .Select(row => row.Split(')')).ToList();

        var graph = new PathFinding.Graph<string>();

        input.ForEach(planets => graph.Add((planets[0], planets[1])));

        var com = input.Select(planets => planets[0]).Except(input.Select(planets => planets[1])).Single();

        var result1 = graph.Nodes.Keys.Sum(planet => graph.BreadthFirstSearch(com, planet).Count);

        var result2 = graph.BreadthFirstSearch("YOU", "SAN");

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2.Count - 2}");
    }
}