using AdventOfCode;

namespace AdventOfCode2017;

public static class Day12
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(row => row.Split(new string[] { "<->", "," }, StringSplitOptions.TrimEntries)
				.Select(int.Parse));

		var all = data.SelectMany(p => p).ToHashSet();
		var found = new HashSet<int>();

		int count = 0, groups = 0, result1 = 0;

		while (found.Count < all.Count)
		{
			var start = found.Count == 0 ? 0 : all.Except(found).First();

			found.Add(start);

			while (count != found.Count)
			{
				count = found.Count;

				foreach (var connections in data.Where(p => !found.Contains(p.First())))
					if (found.Intersect(connections).Any())
						found.UnionWith(connections);
			}

			groups++;
			result1 = result1 == 0 ? found.Count : result1;
		}

		yield return result1;
		yield return groups;
	}
}