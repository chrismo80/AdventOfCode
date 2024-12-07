namespace AdventOfCode2022;

public static class Day12
{
	public static void Solve()
	{
		var map = File.ReadAllLines("AdventOfCode/2022/12/Input.txt")
			.Select(l => l.Select(c => c).ToArray()).ToArray();

		var start = (Y: 0, X: 0);
		var end = (Y: 0, X: 0);

		for (var row = 0; row < map.Length; row++)
		for (var col = 0; col < map[0].Length; col++)
		{
			if (map[row][col] == 'S')
			{
				start = (col, row);
				map[row][col] = 'a';
			}

			if (map[row][col] == 'E')
			{
				end = (col, row);
				map[row][col] = 'z';
			}
		}

		var search = new PathFinding.Grid<char>()
			{ Map = map, Walkable = (_, _, next, _, _, current, _, _, _) => next - current <= 1 };
		var path = search.AStar(start, end);
		//Console.WriteLine(search.Print('⭕', '⚪'));

		Console.WriteLine($"Part 1: {path.Count()}"); // 517

		var distances = new Dictionary<(int, int), int>();

		for (var row = 0; row < map.Length; row++)
		for (var col = 0; col < map[0].Length; col++)
			if (map[row][col] == 'a')
				distances[(col, row)] = int.MaxValue;

		Parallel.ForEach(distances.Keys, start =>
		{
			var search = new PathFinding.Grid<char>()
			{
				Map = map,
				Walkable = (_, _, next, _, _, current, _, _, _) => next - current <= 1
			};

			var distance = search.AStar(start, end).Count();

			if (distance == 0)
				return;

			lock (distances)
			{
				distances[start] = distance;
			}

			//Console.WriteLine($"{start}: {distance}");
		});

		var result2 = distances.OrderBy(kvp => kvp.Value).First();

		path = search.AStar(result2.Key, end);
		//Console.WriteLine(search.Print('⭕', '⚪'));

		Console.WriteLine($"Part 2: {result2.Value}"); // 512
	}
}