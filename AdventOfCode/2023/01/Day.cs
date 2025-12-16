using AdventOfCode;

namespace AdventOfCode2023;

public static class Day1
{
	private static string[] _names =
		["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		yield return lines.Sum(GetCalibrationValue);
		yield return lines.Sum(GetRealCalibrationValue);
	}

	private static long GetCalibrationValue(string text)
	{
		var digits = text.Where(c => char.IsAsciiDigit(c));

		return long.Parse($"{digits.First()}{digits.Last()}");
	}

	private static long GetRealCalibrationValue(string text)
	{
		for (var i = 0; i < _names.Length; i++)
			text = text.Replace(_names[i], $"{_names[i]}{i}{_names[i]}");

		return GetCalibrationValue(text);
	}
}