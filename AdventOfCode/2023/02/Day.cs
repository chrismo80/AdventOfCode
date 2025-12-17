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

		return new Game(id, rounds.Select(GetColors).ToList());
	}

	private static (int, int, int) GetColors(string round)
	{
		var colors = new Dictionary<string, int>
		{
			{ "red", 0 },
			{ "green", 0 },
			{ "blue", 0 }
		};

		foreach (var color in round.Split(", "))
			colors[color.Split(' ').Last()] = int.Parse(color.Split(' ').First());

		return (colors["red"], colors["green"], colors["blue"]);
	}
}