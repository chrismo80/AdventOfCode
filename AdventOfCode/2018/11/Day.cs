namespace AdventOfCode2018;

public static class Day11
{
	public static IEnumerable<object> Solve(string input)
	{
		var sn = int.Parse(input);

		int size = 300, square = 0;

		var result = (0, 0, 0, 0L);
		long maxPower = 0;

		while (square++ < 300)
			foreach (var (x, y) in from y in Enumerable.Range(0, size - square + 1) from x in Enumerable.Range(0, size - square + 1) select (x, y))
			{
				var powerLevel = 0;

				foreach (var (h, v) in from h in Enumerable.Range(0, square) from v in Enumerable.Range(0, square) select (h, v))
					powerLevel += ((x + h + 10) * (y + v) + sn) * (x + h + 10) % 1000 / 100 - 5;

				if (powerLevel > maxPower)
					Console.WriteLine((x, y, square, maxPower));

				result = powerLevel > maxPower ? (x, y, square, maxPower) : result;

				maxPower = Math.Max(maxPower, powerLevel);
			}

		yield return result;
	}
}