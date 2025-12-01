using AdventOfCode;

namespace AdventOfCode2015;

using Extensions;

public static class Day9
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(row => row.Split(' '))
			.Select(words => (From: words[0], To: words[2], Distance: int.Parse(words[4]))).ToList();

		var distances = new Dictionary<(string, string), int>();
		data.ForEach(edge => distances[(edge.From, edge.To)] = distances[(edge.To, edge.From)] = edge.Distance);

		var locations = data.Select(d => d.From).Concat(data.Select(d => d.To)).Distinct();
		var routes = locations.Permutations().OrderBy(TotalDistance).ToHashSet();

		yield return TotalDistance(routes.First());
		yield return TotalDistance(routes.Last());

		int TotalDistance(IEnumerable<string> route)
		{
			return Enumerable.Range(0, route.Count() - 1)
				.Sum(i => distances[(route.ElementAt(i), route.ElementAt(i + 1))]);
		}
	}
}