namespace PathFinding;

public class Grid<T>
{
	private class Node
	{
		public Node? Previous;
		public int X, Y, Cost, Distance;
		public int Priority => Cost + Distance;

		public void Init(Node end, int cost = 0, Node? previous = null)
		{
			Previous = previous;
			Cost = 3 * cost + (previous?.Cost ?? 0);
			Distance = Math.Abs(end.X - X) + Math.Abs(end.Y - Y);
		}
	}

	private readonly PriorityQueue<Node, int> Active = new();

	private readonly HashSet<(int, int)> Visited = new();

	public required T[][] Map { get; set; }

	///<summary>cost of walking to neighbor (default: 1)</summary>
	public Func<T, int>? Cost { get; set; } = (_) => 1;

	///<summary>defines if neighbor is walkable (default: same value as current)</summary>
	public Func<int, int, T, int, int, T, int, int, T, bool>? Walkable { get; set; } =
		(nX, nY, neighbor, cX, cY, current, lX, lY, last) => neighbor!.Equals(current);

	public List<(int X, int Y)> Path { get; private set; } = new();

	public int Width => Map[0].Length;

	public int Height => Map.Length;

	public IEnumerable<(int X, int Y)> AStar((int X, int Y) start, (int X, int Y) end)
	{
		Path.Clear();
		Visited.Clear();
		Active.Clear();

		var current = new Node() { X = start.X, Y = start.Y };
		var target = new Node() { X = end.X, Y = end.Y };

		var last = new Node();

		current.Init(target);
		Visited.Add(Pos(current));
		Active.Enqueue(current, current.Priority);

		while (Active.Count > 0)
		{
			current = Active.Dequeue();

			if (Pos(current) == Pos(target))
				return Path = ReversePath(current).Reverse().ToList();

			foreach (var neighbor in Neighbors(current).Where(n => Walkable!(
						n.X, n.Y, Value(n),
						current.X, current.Y, Value(current),
						last.X, last.Y, Value(last)
					)))
				if (Visited.Add(Pos(neighbor)))
				{
					neighbor.Init(target, Cost!(Value(neighbor)), current);
					Active.Enqueue(neighbor, neighbor.Priority);
					last = current;
				}
		}

		return Path;
	}

	public IEnumerable<(int X, int Y)> BreadthFirstSearch((int X, int Y) start, (int X, int Y) end,
		(int X, int Y) last = default)
	{
		Path.Clear();
		Visited.Clear();

		var previous = new Dictionary<(int X, int Y), (int X, int Y)>();
		var active = new Queue<(int, int)>();

		Visited.Add(start);
		active.Enqueue(start);
		previous[start] = last;
		var current = start;

		while (active.Count > 0)
		{
			current = active.Dequeue();

			if (current.Equals(end))
				break;

			foreach (var neighbor in Neighbors(current.X, current.Y).Where(neighbor =>
						Walkable!(
							neighbor.X, neighbor.Y, Value(neighbor),
							current.X, current.Y, Value(current),
							previous[current].X, previous[current].Y, Value(previous[current])
						) && Visited.Add(neighbor)))
			{
				previous[neighbor] = current;
				active.Enqueue(neighbor);
			}
		}

		return Path = ReversePath(current, start, previous).Reverse().ToList();
	}

	public IEnumerable<(int X, int Y)> Find(Func<T, bool> predicate)
	{
		foreach (var (x, y) in from y in Enumerable.Range(0, Map.Length)
				from x in Enumerable.Range(0, Map[0].Length)
				select (x, y))
			if (predicate(Map[y][x]))
				yield return (x, y);
	}

	public IEnumerable<T> GetSymbols() => Map.SelectMany(x => x).Distinct();

	private T Value((int X, int Y) node) => Map[node.Y][node.X];
	private T Value(Node node) => Value((node.X, node.Y));
	private static (int, int) Pos(Node node) => (node.X, node.Y);

	private IEnumerable<Node> Neighbors(Node current)
	{
		foreach (var (x, y) in Neighbors(current.X, current.Y))
			yield return new Node { X = x, Y = y };
	}

	private IEnumerable<(int X, int Y)> Neighbors(int x, int y)
	{
		if (y > 0) yield return (x, y - 1);
		if (x > 0) yield return (x - 1, y);
		if (y < Map.Length - 1) yield return (x, y + 1);
		if (x < Map[0].Length - 1) yield return (x + 1, y);
	}

	private static IEnumerable<(int X, int Y)> ReversePath((int X, int Y) current, (int X, int Y) start,
		Dictionary<(int, int), (int, int)> previous)
	{
		while (!current.Equals(start))
		{
			yield return current;
			current = previous[current];
		}
	}

	private static IEnumerable<(int X, int Y)> ReversePath(Node current)
	{
		while (current.Previous != null)
		{
			yield return (current.X, current.Y);
			current = current.Previous;
		}
	}

	public string Print(char path = '.', char wall = default, char visited = default, char active = '?')
	{
		var b = new System.Text.StringBuilder();

		foreach (var (x, y) in from y in Enumerable.Range(0, Map.Length)
				from x in Enumerable.Range(0, Map[0].Length + 1)
				select (x, y))
			b.Append(x >= Map[0].Length ? '\n' :
				Path.Contains((x, y)) ? path :
				visited != default && Active.UnorderedItems.Any(v => v.Element.X == x && v.Element.Y == y) ? active :
				visited != default && Visited.Contains((x, y)) ? visited :
				Walkable!(x, y, Map[y][x], x, y, Map[y][x], x, y, Map[y][x]) ? '➰' :
				wall == default(char) ? Map[y][x] : wall);

		var efficiency = Path.Any() ? (double)Path.Count / Visited.Count : 0;
		return b.ToString() + (Path.Any() ? $"Shortest path: {Path.Count} ({efficiency:P1})" : "");
	}
}