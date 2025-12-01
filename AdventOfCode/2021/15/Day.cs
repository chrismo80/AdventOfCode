using AdventOfCode;

namespace AdventOfCode2021;

public static class Day15
{
	public static IEnumerable<object> Solve(string input)
	{
		var map = input.Lines()
			.Select(row => row.Select(c => c - '0').ToArray()).ToArray();

		var search = new PathFinding.Grid<int>
		{
			Map = map,
			Walkable = (_, _, _, _, _, _, _, _, _) => true,
			Cost = (neighbor) => neighbor
		};

		var path = search.AStar((0, 0), (99, 99));
		var result1 = path.Sum(p => map[p.Y][p.X]);
		//Console.WriteLine(search.Print());

		var row = map.Select(row => row
			.Concat(row.Select(r => r + 1))
			.Concat(row.Select(r => r + 2))
			.Concat(row.Select(r => r + 3))
			.Concat(row.Select(r => r + 4)));

		var map2 = row
			.Concat(row.Select(row => row.Select(r => r + 1)))
			.Concat(row.Select(row => row.Select(r => r + 2)))
			.Concat(row.Select(row => row.Select(r => r + 3)))
			.Concat(row.Select(row => row.Select(r => r + 4)))
			.Select(row => row.Select(r => r > 9 ? r - 9 : r).ToArray()).ToArray();

		search.Map = map2;

		path = search.AStar((0, 0), (499, 499));
		var result2 = path.Sum(p => map2[p.Y][p.X]);
		//Console.WriteLine(search.Print());

		yield return result1;
		yield return result2;
	}
}