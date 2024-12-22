using System.Text.RegularExpressions;
using AdventOfCode;

namespace AdventOfCode2024;

public static class Day4
{
	public static void Solve()
	{
		var input = Helper.Load(2024, 4).ToMap();

		var result1 = GetLines(input)
			.Select(line => Regex.Matches(line, "XMAS").Count)
			.Sum();

		var corners = new[] { "MMSS", "MSSM", "SSMM", "SMMS" };

		var result2 = FindChar(input, 'A')
			.Select(center => FindDiagonalNeighbors(input, center))
			.Count(neighbors => corners.Contains(neighbors));

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static IEnumerable<(int X, int Y)> FindChar(char[][] input, char c)
	{
		for (var y = 0; y < input.Length; y++)
		for (var x = 0; x < input[0].Length; x++)
			if (input[y][x] == c)
				yield return (x, y);
	}

	private static string FindDiagonalNeighbors(char[][] input, (int X, int Y) location)
	{
		var neighbors = "";

		if (location.X > 0 && location.Y > 0)
			neighbors += input[location.Y - 1][location.X - 1];

		if (location.X < input[0].Length - 1 && location.Y > 0)
			neighbors += input[location.Y - 1][location.X + 1];

		if (location.X < input[0].Length - 1 && location.Y < input.Length - 1)
			neighbors += input[location.Y + 1][location.X + 1];

		if (location.X > 0 && location.Y < input.Length - 1)
			neighbors += input[location.Y + 1][location.X - 1];

		return neighbors;
	}

	private static IEnumerable<string> GetLines(char[][] input)
	{
		for (var y = 0; y < input.Length; y++)
		{
			yield return GetText(input, 0, y, 1, 0);
			yield return GetText(input, input[0].Length - 1, y, -1, 0);

			yield return GetText(input, 0, y, 1, -1);
			yield return GetText(input, input[0].Length - 1, y, -1, 1);

			yield return GetText(input, 0, y, 1, 1);
			yield return GetText(input, input[0].Length - 1, y, -1, -1);
		}

		for (var x = 0; x < input[0].Length; x++)
		{
			yield return GetText(input, x, 0, 0, 1);
			yield return GetText(input, x, input.Length - 1, 0, -1);

			if (x == 0 || x == input.Length - 1)
				continue;

			yield return GetText(input, x, 0, -1, 1);
			yield return GetText(input, x, input.Length - 1, 1, -1);

			yield return GetText(input, x, 0, 1, 1);
			yield return GetText(input, x, input.Length - 1, -1, -1);
		}
	}

	private static string GetText(char[][] input, int x, int y, int xOffset, int yOffset)
	{
		var text = "";

		while (x >= 0 && x < input[0].Length && y >= 0 && y < input.Length)
		{
			text += input[y][x];

			x += xOffset;
			y += yOffset;
		}

		return text;
	}
}