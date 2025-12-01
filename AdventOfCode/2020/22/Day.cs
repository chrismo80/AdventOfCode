using AdventOfCode;

namespace AdventOfCode2020;

public static class Day22
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.ToArray<string>("\n");

		var player1 = new Queue<int>();
		var player2 = new Queue<int>();

		lines.Chunk(lines.Length / 2 + 1).First().Skip(1).SkipLast(1).Select(int.Parse).ToList()
			.ForEach(card => player1.Enqueue(card));

		lines.Chunk(lines.Length / 2 + 1).Last().Skip(1).Select(int.Parse).ToList()
			.ForEach(card => player2.Enqueue(card));

		while (player1.TryDequeue(out var card1) && player2.TryDequeue(out var card2))
			if (card1 > card2)
			{
				player1.Enqueue(card1);
				player1.Enqueue(card2);
			}
			else
			{
				player2.Enqueue(card2);
				player2.Enqueue(card1);
			}

		yield return player1.Concat(player2).Reverse().Select((card, i) => card * (i + 1)).Sum();
	}
}