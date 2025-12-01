using AdventOfCode;

namespace AdventOfCode2024;

public static class Day18
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToNestedArray<int>("\n", ",")
			.Select(d => (d.First(), d.Last())).ToArray();

		var walkable = ((int, int) pos) => pos.Neighbors(71, 71)
			.Where(n => !data.Take(1024).Contains(n));

		yield return walkable.Bfs((0, 0), (70, 70)).Count();
		yield return FindCutOff(data);
	}

	private static (int, int) FindCutOff((int, int)[] bytes)
	{
		var c = bytes.Length;

		while (c-- > 0)
		{
			var walkable = ((int, int) pos) => pos.Neighbors(71, 71)
				.Where(n => !bytes.Take(c).Contains(n));

			if (walkable.Bfs((0, 0), (70, 70)).Any())
				return bytes[c];
		}

		return default;
	}
}