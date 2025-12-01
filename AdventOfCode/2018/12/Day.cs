using AdventOfCode;

namespace AdventOfCode2018;

public static class Day12
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		var row = lines[0].Split(' ')[2];
		var notes = lines.Skip(2).Select(note =>
			(Match: note.Split(" => ")[0], Result: note.Split(" => ")[1]));

		int zeroIndex = 10, generations = 1_000;
		row = ".........." + row;

		Console.WriteLine(row);

		while (generations-- > 0)
		{
			if (!row.EndsWith("....."))
				row += "..................................................";

			var pots = new System.Text.StringBuilder(row);

			for (var i = 2; i < row.Length - 2; i++)
			{
				var pattern = row.Substring(i - 2, 5);
				var match = notes.FirstOrDefault(note => note.Match == pattern);

				if (match != (null, null))
					pots[i] = match.Result[0];
				else
					pots[i] = '.';
			}

			row = pots.ToString();

			if (generations % 100 == 0)
			{
				var result = row
					.Select((plant, pos) => (plant, pos))
					.Where(pot => pot.plant == '#')
					.Sum(pot => pot.pos - zeroIndex);

				Console.WriteLine($"{generations}: {result - 798}");
			}
		}

		yield return row.Select((plant, pos) => (plant, pos))
			.Where(pot => pot.plant == '#')
			.Sum(pot => pot.pos - zeroIndex);

		yield return 500_000_000L * 8100 + 798;
	}
}