using System.Text.RegularExpressions;

namespace AdventOfCode;

public static class StringExtensions
{
	public static T[] ToArray<T>(this string input, string delimiter) =>
		input.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => (T)Convert.ChangeType(item, typeof(T)))
			.ToArray();

	public static T[][] ToNestedArray<T>(this string input, string delim1, string delim2) =>
		input.Split(delim1, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => item.ToArray<T>(delim2))
			.ToArray();

	public static T[][][] ToNestedArray<T>(this string input, string d1, string d2, string d3) =>
		input.Split(d1, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => item.ToNestedArray<T>(d2, d3))
			.ToArray();

	public static char[][] ToMap(this string input) =>
		input.Split('\n').Select(row => row.ToArray()).ToArray();

	public static IEnumerable<string[]> Match(this string[] lines, Regex regex) =>
		lines.Select(line => regex.Match(line).Groups.Values.Skip(1).Select(v => v.Value).ToArray());
}

public static class Input
{
	public static string Load(int year, int day) =>
		File.ReadAllText(FileName(year, day));

	public static string[] LoadLines(int year, int day) =>
		File.ReadAllLines(FileName(year, day));

	private static string FileName(int year, int day) =>
		$"AdventOfCode/{year}/{day:00}/Input.txt";
}