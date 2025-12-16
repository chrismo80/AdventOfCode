using AdventOfCode;

namespace AdventOfCode2023;

public static class Day2
{
	private record Game(int Id, List<(int R, int G, int B)> Sets);

	public static IEnumerable<object> Solve(string input)
	{
		var games = input.Lines().Select(ToGame).ToList();

		yield return games.Where(game => game.IsPossible(12, 13, 14)).Sum(game => game.Id);
		yield return games.Sum(Power);
	}

	private static bool IsPossible(this Game game, int r, int g, int b)
	{
		return game.Sets.Select(s => s.R).All(x => x <= r)
			&& game.Sets.Select(s => s.G).All(x => x <= g)
			&& game.Sets.Select(s => s.B).All(x => x <= b);
	}

	private static int Power(this Game game)
	{
		return game.Sets.Select(s => s.R).Max()
			* game.Sets.Select(s => s.G).Max()
			* game.Sets.Select(s => s.B).Max();
	}

	private static Game ToGame(this string line)
	{
		var id = int.Parse(line.Split(": ").First().Split(' ').Last());
		var rounds = line.Split(": ").Last().Split("; ");

		var colors = new List<(int, int, int)>();

		foreach (var round in rounds)
		{
			int r = 0, g = 0, b = 0;

			foreach (var color in round.Split(", "))
				switch (color.Split(' ').Last())
				{
					case "red": r = int.Parse(color.Split(' ').First()); break;
					case "green": g = int.Parse(color.Split(' ').First()); break;
					case "blue": b = int.Parse(color.Split(' ').First()); break;
				}

			colors.Add((r, g, b));
		}

		return new Game(id, colors);
	}
}