using AdventOfCode;

namespace AdventOfCode2015;

using System.Text.RegularExpressions;

public static class Day8
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		var sizeInCode = lines.Select(text => text.Length);

		var memory = lines
			.Select(text => Regex.Unescape(text))
			.Select(text => text[1..^1].Replace(@"\", "-").Replace("\"", "-"));

		var sizeInMemory = memory.Select(text => text.Length);

		var encoded = lines
			.Select(text => text.Replace("\\", @"\\").Replace("\"", @"\"""))
			.Select(text => "\"" + text + "\"");

		var sizeEncoded = encoded.Select(text => text.Length);

		yield return sizeInCode.Sum() - sizeInMemory.Sum();
		yield return sizeEncoded.Sum() - sizeInCode.Sum();
	}
}