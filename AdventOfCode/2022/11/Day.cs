namespace AdventOfCode2022;

using Extensions;

public static class Day11
{
	public static IEnumerable<object> Solve(string input)
	{
		var monkeys = input.Split("\n\n")
			.Select(monkey => monkey.Split("\n").Select(line =>
				line.Contains("Starting") ? line.Split("items: ")[1] :
				line.Contains("Operation:") ? line.Split("new = ")[1] :
				line.Contains("Test:") ? line.Split("divisible by ")[1] :
				line.Contains("If") ? line.Split("throw to monkey ")[1] : "0").ToArray()).ToArray();

		var lcm = monkeys.Product(monkey => int.Parse(monkey[3])); // primes

		for (var i = 1; i <= 10_000; i++)
			foreach (var monkey in monkeys)
			{
				foreach (var item in monkey[1].Split(",").Where(x => x != ""))
				{
					// perform operation to get worry level for item
					var worryLevel = monkey[2].Replace("old", $"{item}.0").Evaluate<long>();

					// get test result for new monkey to throw item to
					var next = (int)Math.Min(1, worryLevel % int.Parse(monkey[3])) + 4;

					// copy item to other monkey (with mod trick to prevent overflow)
					monkeys[int.Parse(monkey[next])][1] += $",{worryLevel % lcm}";

					// increase inspected counter of this monkey
					monkey[0] = (int.Parse(monkey[0]) + 1).ToString();
				}

				// all items thrown, delete items from this monkey
				monkey[1] = "";
			}

		yield return monkeys.Select(monkey => long.Parse(monkey[0])).Order().TakeLast(2).Product();
	}
}