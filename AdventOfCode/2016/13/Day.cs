namespace AdventOfCode2016;

public static class Day13
{
	public static IEnumerable<object> Solve(string _)
	{
		const int width = 50, height = 50, input = 1350;

		var grid = Enumerable.Repeat(0, height)
			.Select(_ => Enumerable.Repeat(0, width).ToArray()).ToArray();

		foreach (var (x, y) in from y in Enumerable.Range(0, height)
				from x in Enumerable.Range(0, width)
				select (x, y))
		{
			var bitMask = Convert.ToString(x * x + 3 * x + 2 * x * y + y + y * y + input, 2);

			grid[y][x] = bitMask.Count(bit => bit == '1') % 2 == 0 ? 0 : 1;
		}

		var search = new PathFinding.Grid<int> { Map = grid };
		var result1 = search.BreadthFirstSearch((1, 1), (31, 39)).ToList();
		Console.WriteLine(search.Print('⭕', '⚪', '❕', '❔'));

		var result2 = new HashSet<(int, int)>();

		foreach (var (x, y) in from y in Enumerable.Range(0, height)
				from x in Enumerable.Range(0, width)
				where grid[y][x] == 0
				select (x, y))
		{
			var path = search.BreadthFirstSearch((1, 1), (x, y));

			if (path.Any() && path.Count() <= 50) result2.Add(path.Last());
			//Console.WriteLine(search.Print('⭕', '⚪', '❕', '❔'));
		}

		yield return result1;
		yield return result2.Count + 1;
	}
}