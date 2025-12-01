using AdventOfCode;

namespace AdventOfCode2019;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(row => row.Split(')')).ToList();

		var graph = new PathFinding.Graph<string>();

		data.ForEach(planets => graph.Add((planets[0], planets[1])));

		var com = data.Select(planets => planets[0]).Except(data.Select(planets => planets[1])).Single();

		yield return graph.Nodes.Keys.Sum(planet => graph.BreadthFirstSearch(com, planet).Count);
		yield return graph.BreadthFirstSearch("YOU", "SAN").Count - 2;
	}
}