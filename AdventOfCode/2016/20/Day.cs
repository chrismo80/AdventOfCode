namespace AdventOfCode2016;

public static class Day20
{
	public static IEnumerable<object> Solve(string input)
	{
		var ranges = File.ReadAllLines("AdventOfCode/2016/20/Input.txt")
			.Select(row => row.Split('-').Select(long.Parse))
			.Select(range => (min: range.First(), max: range.Last()))
			.OrderBy(range => range.min).ToArray();

		long result1 = 0, result2 = 0, lastBlocked = 0;

		foreach (var (min, max) in ranges)
		{
			if (min - lastBlocked > 1)
				result2 += min - lastBlocked - 1;

			result1 = result1 > 0 || result2 == 0 ? result1 : lastBlocked + 1;

			lastBlocked = Math.Max(max, lastBlocked);
		}

		result2 += (long)Math.Pow(2, 32) - 1 - lastBlocked; // 4294967295 - lastBlocked

		yield return result1;
		yield return result2;
	}
}