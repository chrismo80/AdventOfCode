namespace PathFinding;
public class Graph<T> where T : notnull
{
    public Dictionary<T, Dictionary<T, int>> Nodes = new();
    public Dictionary<T, T> Previous = new();

    public void Add((T, T) edge, int distance = 1)
    {
        if (!Nodes.ContainsKey(edge.Item1)) Nodes[edge.Item1] = new Dictionary<T, int>();
        if (!Nodes.ContainsKey(edge.Item2)) Nodes[edge.Item2] = new Dictionary<T, int>();

        Nodes[edge.Item1][edge.Item2] = distance;
        Nodes[edge.Item2][edge.Item1] = distance;
    }

    public IEnumerable<(T Node, int Distance)> Dijkstra(T start, T end)
    {
        Previous.Clear();

        var active = Nodes.Keys.ToList();
        var distances = Nodes.Keys.ToDictionary(key => key, _ => int.MaxValue);
        distances[start] = 0;
        var current = start;

        while (active.Count > 0 && distances[current] < int.MaxValue)
        {
            current = active.OrderBy(node => distances[node]).First();
            active.Remove(current);

            if (current.Equals(end))
                return ReversePath(start, end, distances).ToList();

            foreach (var neighbor in Nodes[current].Where(neighbor =>
                distances[neighbor.Key] > distances[current] + neighbor.Value))
            {
                distances[neighbor.Key] = distances[current] + neighbor.Value;
                Previous[neighbor.Key] = current;
            }
        }

        return Enumerable.Empty<(T, int)>();
    }

    public List<T> BreadthFirstSearch(T start, T end)
    {
        Previous.Clear();

        var active = new Queue<T>();
        active.Enqueue(start);

        while (active.TryDequeue(out T? current) && !end.Equals(current))
        {
            foreach (var neighbor in Nodes[current].Where(n => !Previous.ContainsKey(n.Key)))
            {
                Previous[neighbor.Key] = current;
                active.Enqueue(neighbor.Key);
            }
        }

        return ReversePath(start, end).Reverse().ToList();
    }

    public List<T> DepthFirstSearch(T start, T end)
    {
        Previous.Clear();

        var active = new Stack<T>();
        active.Push(start);

        while (active.TryPop(out T? current) && !end.Equals(current))
        {
            foreach (var neighbor in Nodes[current].Where(n => !Previous.ContainsKey(n.Key)))
            {
                Previous[neighbor.Key] = current;
                active.Push(neighbor.Key);
            }
        }

        return ReversePath(start, end).Reverse().ToList();
    }

    public IEnumerable<IEnumerable<T>> AllPaths(T current, T end,
        Func<IEnumerable<T>, T, bool>? condition = null, IEnumerable<T>? path = null)
    {
        path ??= Enumerable.Empty<T>();

        if (path.Contains(current) && (condition == null || !condition(path, current))) yield break;

        path = path.Append(current);

        if (current.Equals(end)) { yield return path; yield break; }

        foreach (var neighbor in Nodes[current])
            foreach (var subPath in AllPaths(neighbor.Key, end, condition, path)) yield return subPath;
    }

    private IEnumerable<T> ReversePath(T start, T current)
    {
        while (!current.Equals(start))
        {
            yield return current;
            current = Previous[current];
        }
    }

    private IEnumerable<(T, int)> ReversePath(T start, T current, Dictionary<T, int> distances)
    {
        while (!current.Equals(start))
        {
            yield return (current, distances[current]);
            current = Previous[current];
        }
    }
}