using AdventOfCode;

namespace AdventOfCode2019;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<int>("\n");

		yield return data.Sum(mass => mass / 3 - 2);
		yield return data.Sum(mass => GetTotalFuel(mass));

		static int GetTotalFuel(int mass)
		{
			var fuel = mass / 3 - 2;
			var total = fuel;

			while (fuel > 0)
			{
				fuel = fuel / 3 - 2;
				total += Math.Max(fuel, 0);
			}

			return total;
		}
	}
}