namespace AdventOfCode2016;

using Extensions;

public static class Day24
{
	public static void Solve()
	{
		var map = File.ReadAllLines("AdventOfCode/2016/24/Input.txt").Select(row => row.ToArray()).ToArray();

		var search = new PathFinding.Grid<char>() { Map = map, Walkable = (_, _, next, _) => next != '#' };
		var distances = new Dictionary<(int From, int To), int>();

		// get node positions from map (for BFS start/end)
		var nodes = (from y in Enumerable.Range(0, map.Length)
			from x in Enumerable.Range(0, map[0].Length)
			where char.IsDigit(map[y][x])
			select (x, y)).ToDictionary(node => map[node.y][node.x] - '0', node => (node.x, node.y));

		// calculate the distances for all possible edges
		foreach (var (from, to) in from f in nodes.Keys from t in nodes.Keys where f < t select (f, t))
			distances[(from, to)] = distances[(to, from)] = search.BreadthFirstSearch(nodes[from], nodes[to]).Count();

		// get all node permutations except node 0 (append/prepend it later as start/end points)
		var routes = nodes.Keys.Where(n => n != 0).Permutations();

		var route1 = routes.Select(route => route.Prepend(0)).OrderBy(TotalDistance).First();
		var route2 = routes.Select(route => route.Prepend(0).Append(0)).OrderBy(TotalDistance).First();

		Console.WriteLine($"Part 1: {TotalDistance(route1)} ({string.Join('-', route1)})");
		Console.WriteLine($"Part 2: {TotalDistance(route2)} ({string.Join('-', route2)})");

		// calc the total distance for a specific route (combination of nodes based on distances dictionary)
		int TotalDistance(IEnumerable<int> route)
		{
			return Enumerable.Range(0, route.Count() - 1)
				.Sum(i => distances[(route.ElementAt(i), route.ElementAt(i + 1))]);
		}
	}
}