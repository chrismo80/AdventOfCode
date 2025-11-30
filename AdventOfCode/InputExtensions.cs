using System.Text.RegularExpressions;

namespace AdventOfCode;

public static class File
{
	public static string Name(int year, int day, string file) =>
		$"../../../AdventOfCode/{year}/{day:00}/{file}.txt";
}

public static class Input
{
	public static string Load(int year, int day, string file = "Input") =>
		System.IO.File.ReadAllText(File.Name(year, day, file));

	public static bool Exists(int year, int day, string file = "Input") =>
		System.IO.File.Exists(File.Name(year, day, file));
}

public static class Output
{
	public static void Save(int year, int day, string content, string file = "Output") =>
		System.IO.File.WriteAllText(File.Name(year, day, file), content);
}

public static class InputExtensions
{
	public static string[] Lines(this string input) =>
		input.Split('\n');

	public static T[] ToArray<T>(this string input, string delimiter) =>
		input.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)
			.ConvertTo<T>()
			.ToArray();

	public static T[][] ToNestedArray<T>(this string input, string delim1, string delim2) =>
		input.Split(delim1, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => item.ToArray<T>(delim2))
			.ToArray();

	public static T[][][] ToNestedArray<T>(this string input, string d1, string d2, string d3) =>
		input.Split(d1, StringSplitOptions.RemoveEmptyEntries)
			.Select(item => item.ToNestedArray<T>(d2, d3))
			.ToArray();

	public static IEnumerable<T[]> Match<T>(this string[] lines, Regex regex) =>
		lines.Select(line =>
			regex.Match(line).Groups.Values.Skip(1)
				.Select(v => v.Value).ConvertTo<T>().ToArray());

	public static IEnumerable<string[]> Match(this string[] lines, Regex regex) =>
		lines.Match<string>(regex);

	public static IEnumerable<T> ConvertTo<T>(this IEnumerable<string> items) =>
		items.Select(item => (T)Convert.ChangeType(item, typeof(T)));

	public static IEnumerable<string> Transpose(this IEnumerable<string> lines) =>
		Enumerable.Range(0, lines.First().Length)
			.Select(i => new string(lines.Select(line => line[i]).ToArray()));
}