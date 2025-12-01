using AdventOfCode;

namespace AdventOfCode2021;

public static class Day10
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(TrimLines).Where(l => l != "");

		yield return data.Sum(CorruptPoints);

		var points = data.Select(IncompletePoints).Where(p => p > 0).Order();

		yield return points.Chunk(points.Count() / 2 + 1).First().Last();
	}

	private static string TrimLines(string line)
	{
		var length = 0;
		while (line.Length > 0 && line.Length != length)
		{
			length = line.Length;
			line = line.Replace("()", "").Replace("[]", "").Replace("{}", "").Replace("<>", "");
		}

		return line;
	}

	private static long CorruptPoints(string line)
	{
		var c = line.Intersect(")]}>".ToArray()).FirstOrDefault();
		return c == ')' ? 3 : c == ']' ? 57 : c == '}' ? 1197 : c == '>' ? 25137 : 0;
	}

	private static long IncompletePoints(string line)
	{
		if (line.Intersect(")]}>".ToArray()).Any())
			return 0;

		long points = 0;
		foreach (var c in line.Reverse())
		{
			points *= 5;
			points += c == '(' ? 1 : c == '[' ? 2 : c == '{' ? 3 : c == '<' ? 4 : 0;
		}

		return points;
	}
}