using AdventOfCode;

namespace AdventOfCode2024;

public static class Day22
{
	public static void Solve()
	{
		var secretNumbers = Input.Load(2024, 22).ToArray<long>("\n")
			.Select(n => new List<long>([n])).ToArray();

		for (var c = 0; c < 2000; c++)
		for (var i = 0; i < secretNumbers.Length; i++)
			secretNumbers[i].Add(secretNumbers[i].Last().Next());

		var result1 = secretNumbers.Select(n => n.Last()).Sum();
		var result2 = secretNumbers.BestPrice();

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static long BestPrice(this List<long>[] secretNumbers)
	{
		var priceLists = secretNumbers.Select(list => list.Select(n => n.Price()).ToArray());

		var dict = new Dictionary<string, long>();

		foreach (var prices in priceLists)
		{
			var hash = new HashSet<string>(prices.Length);

			for (var i = 5; i < prices.Length; i++)
			{
				var sequence = string.Join(',', prices[(i - 4)..i].Zip(prices[(i - 5)..(i - 1)], (x, y) => x - y));

				if (hash.Add(sequence))
					dict[sequence] = dict.GetValueOrDefault(sequence) + prices[i - 1];
			}
		}

		return dict.Values.Max();
	}

	private static long Price(this long secretNumber) => secretNumber % 10;

	private static long Next(this long secretNumber) => secretNumber = secretNumber.Step1().Step2().Step3();

	private static long Step1(this long secretNumber) => secretNumber.Mix(secretNumber * 64).Prune();
	private static long Step2(this long secretNumber) => secretNumber.Mix(secretNumber / 32).Prune();
	private static long Step3(this long secretNumber) => secretNumber.Mix(secretNumber * 2048).Prune();

	private static long Mix(this long secretNumber, long value) => secretNumber ^ value;
	private static long Prune(this long secretNumber) => secretNumber % 16777216;
}