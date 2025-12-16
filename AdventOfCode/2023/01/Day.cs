using AdventOfCode;

namespace AdventOfCode2023;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		yield return lines.Sum(GetCalibrationValue);

		string[] names = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

		yield return lines.Sum(line => GetRealCalibrationValue(line, names));
	}

	private static long GetCalibrationValue(string text)
	{
		var digits = text.Where(c => char.IsAsciiDigit(c));

		return long.Parse($"{digits.First()}{digits.Last()}");
	}

	private static long GetRealCalibrationValue(string text, string[] names)
	{
		foreach (var name in names)
			text = text.Replace(name, $"{name}{names.IndexOf(name)}{name}");

		return GetCalibrationValue(text);
	}
}