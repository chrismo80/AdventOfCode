using AdventOfCode;
using Extensions;

namespace AdventOfCode2025;

public static class Day7
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		var beams = new HashSet<int> { lines.First().IndexOf('S') };

		var count = 0;

		foreach (var line in lines.Skip(1))
		{
			var splits = line.AllIndexesOf('^').Intersect(beams).ToList();

			foreach (var split in splits)
			{
				beams.Add(split - 1);
				beams.Remove(split);
				beams.Add(split + 1);
			}

			count += splits.Count;
		}

		yield return count;
	}
}