using Extensions;

namespace AdventOfCode2024;

public static class Day11
{
	public static void Solve()
	{
		var stones = Input.Load(2024, 11)
			.Split(' ').Select(long.Parse).ToDictionary(x => x, x => 1L);

		var result1 = stones.Blink(25).Values.Sum();
		var result2 = stones.Blink(75).Values.Sum();

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static Dictionary<long, long> Blink(this Dictionary<long, long> stones, int count)
	{
		for (var i = 0; i < count; i++)
			stones = stones.Blink();

		return stones;
	}

	private static Dictionary<long, long> Blink(this Dictionary<long, long> list)
	{
		var stones = new Dictionary<long, long>();

		foreach (var stone in list.Keys)
		{
			if (stone == 0)
			{
				stones.Update(1, list[0]);
				continue;
			}

			var text = stone.ToString();

			if (text.Length % 2 == 0)
			{
				var left = text[..(text.Length / 2)];
				var right = text[(text.Length / 2)..];

				stones.Update(long.Parse(left), list[stone]);
				stones.Update(long.Parse(right), list[stone]);

				continue;
			}

			stones.Update(stone * 2024, list[stone]);
		}

		return stones;
	}

	private static void Update(this Dictionary<long, long> list, long key, long value)
	{
		if (!list.TryAdd(key, value))
			list[key] += value;
	}
}