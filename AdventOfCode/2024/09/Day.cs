namespace AdventOfCode2024;

public static class Day9
{
	public static void Solve()
	{
		var input = File.ReadAllText("AdventOfCode/2024/09/Input.txt");

		var diskmap = input.GetDiskMap().ToArray();

		while (diskmap.Move())
		{ }

		var result1 = diskmap.Where(x => x != '.').Select((c, i) => (long)(i * (c - '0'))).Sum();
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static string GetDiskMap(this string input) =>
		string.Join("", input.Select((c, i) => c.Transform(i)));

	private static string Transform(this char item, int index) =>
		new(Enumerable.Repeat(index % 2 == 0 ? (char)('0' + index / 2) : '.', item - '0').ToArray());

	private static bool Move(this char[] list)
	{
		var digit = list.Last(x => x != '.');
		var dot = list.First(x => x == '.');

		var digitIndex = Array.LastIndexOf(list, digit);
		var dotIndex = Array.IndexOf(list, dot);

		if (digitIndex < dotIndex)
			return false;

		list[digitIndex] = dot;
		list[dotIndex] = digit;

		return true;
	}
}