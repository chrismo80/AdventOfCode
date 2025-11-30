namespace AdventOfCode2019;

public static class Day22
{
	public static void Solve()
	{
		var input = File.ReadAllLines("AdventOfCode/2019/22/Input.txt")
			.Select(row => row.Split(' ').TakeLast(2)).ToList();

		int n, i, loop = 1, size = 10_007; //119_315_717_514_047;

		var deck = Enumerable.Range(0, size).ToArray();
		var history = new HashSet<int>();

		while (loop-- > 0)
			foreach (var cmd in input)
				switch (cmd.First())
				{
					case "cut":
						n = int.Parse(cmd.Last());
						deck = n > 0 ? deck.Skip(n).Concat(deck.Take(n)).ToArray() :
							deck.TakeLast(n * -1).Concat(deck.SkipLast(n * -1)).ToArray();
						break;

					case "new":
						deck = deck.AsEnumerable().Reverse().ToArray();
						break;

					case "increment":
						n = int.Parse(cmd.Last());
						i = 0;
						foreach (var card in deck.Select(c => c).ToArray())
						{
							deck[i] = card;
							i += n;
							i %= deck.Length;
						}

						break;
				}

		Console.WriteLine(deck.ToList().IndexOf(2019));

		Console.WriteLine(deck[2020]);
	}
}