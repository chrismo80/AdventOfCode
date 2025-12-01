using AdventOfCode;

namespace AdventOfCode2022;

public static class Day16
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(row => row.Split(' ', '=', ';', ',').Where(w => w != "").ToArray())
			.Select(words => (Name: words[1], Flowrate: int.Parse(words[5]), Tunnels: words.Skip(10)));

		var graph = new PathFinding.Graph<string>();

		data.ToList().ForEach(valve => valve.Tunnels.ToList().ForEach(tunnel =>
			graph.Add((valve.Name, tunnel))));

		List<Valve> Valves = new(), Path = new(), Opened = new(), Check = new();

		foreach (var (Name, Flowrate, Tunnels) in data)
			Valves.Add(new Valve { Name = Name, Flowrate = Flowrate });

		yield return BestPath("AA").ToArray();

		IEnumerable<Valve> BestPath(string start)
		{
			var current = Valves.First(v => v.Name == start);

			Path = new List<Valve> { current };
			Check = new List<Valve> { current };
			Opened = new List<Valve>();
			var minutesLeft = 30;
			var pressure = 0;

			current.Set();

			while (Check.Any())
			{
				current = Check.OrderByDescending(x => x.Value).First();

				if (minutesLeft <= 0)
					return Path = ReversePath(current).Reverse().ToList();

				Check.Remove(current);

				if (!Opened.Any(valve => valve.Name == current.Name) && current.Flowrate > 0)
				{
					pressure += current.MinutesLeft * current.Flowrate;
					current.Open = true;
					Opened.Add(current);
				}

				minutesLeft--;

				var newValves = WalkablePositions(current, minutesLeft);

				Check.AddRange(newValves);
			}

			return Path;
		}

		IEnumerable<Valve> WalkablePositions(Valve current, int minutesLeft)
		{
			var neighbors = graph.Nodes[current.Name];

			foreach (var valve in Valves.Where(v => neighbors.ContainsKey(v.Name)))
				valve.Set(minutesLeft, current, current.Cost + 1);

			return Valves.Where(v => neighbors.ContainsKey(v.Name) && !Check.Any(c => c.Name == v.Name));
		}
	}

	private static IEnumerable<Valve> ReversePath(Valve current)
	{
		do
		{
			yield return current;
			current = current.Parents.Pop()!;
		} while (current != null);
	}

	public class Valve
	{
		public bool Open;
		public string Name = "";
		public int Cost, MinutesLeft, Flowrate;
		public Stack<Valve> Parents = new();
		public int Value => MinutesLeft + Cost;

		public void Set(int minutesLeft = 30, Valve? parent = null, int cost = 0)
		{
			MinutesLeft = minutesLeft;
			Parents.Push(parent!);
			Cost = cost;
		}
	}
}