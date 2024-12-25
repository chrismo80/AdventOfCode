using AdventOfCode;

namespace AdventOfCode2024;

public static class Day18
{
	public static void Solve()
	{
		var input = Input.Load(2024, 18).ToNestedArray<int>("\n", ",")
			.Select(d => (d.First(), d.Last())).ToArray();

		var walkable = ((int, int) pos) => pos.Neighbors(71, 71)
			.Where(n => !input.Take(1024).Contains(n));

		var result1 = walkable.Bfs((0, 0), (70, 70)).Count();
		var result2 = FindCutOff(input);

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
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