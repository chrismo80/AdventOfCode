using AdventOfCode;

namespace AdventOfCode2021;

public static class Day12
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines().Select(row => row.Split('-'));

		var graph = new PathFinding.Graph<string>();

		lines.ToList().ForEach(edge => graph.Add((edge[0], edge[1])));

		bool Part1(IEnumerable<string> path, string current)
		{
			return current.All(c => char.IsUpper(c));
		}

		bool Part2(IEnumerable<string> path, string current)
		{
			return current.All(c => char.IsUpper(c) ||
				(path.Where(v => v.All(c => char.IsLower(c))).GroupBy(v => v).Max(g => g.Count()) <= 1 &&
					current != "start"));
		}

		yield return graph.AllPaths("start", "end", Part1);
		yield return graph.AllPaths("start", "end", Part2);

		var result0 = graph.BreadthFirstSearch("start", "end").Count;
	}
}