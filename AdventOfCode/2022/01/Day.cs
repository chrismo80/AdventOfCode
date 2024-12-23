namespace AdventOfCode2022;

using Extensions;

public static class Day1
{
	public static void Solve()
	{
		var data = File.ReadAllLines("AdventOfCode/2022/01/Input.txt")
			.SplitBy(row => string.IsNullOrEmpty(row))
			.Select(elf => elf.Select(cal => int.Parse(cal)).Sum())
			.OrderByDescending(elf => elf);

		Console.WriteLine($"Part 1: {data.First()}, Part 2: {data.Take(3).Sum()}");
	}
}