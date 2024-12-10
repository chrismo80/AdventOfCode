namespace AdventOfCode2024;

public static class Day9
{
	public static void Solve()
	{
		var diskmap = File.ReadAllText("AdventOfCode/2024/09/Input.txt")
			.GetDiskMap();

		var filesystem = diskmap.ToArray();

		while (filesystem.MoveSingle())
		{ }

		var result1 = filesystem.GetCheckSum();

		filesystem = diskmap.ToArray();

		while (filesystem.MoveBlock())
		{ }

		var result2 = filesystem.GetCheckSum();

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static long GetCheckSum(this char[] diskmap) =>
		diskmap.Where(x => x != '.').Select((c, i) => (long)(i * (c - '0'))).Sum();

	private static string GetDiskMap(this string input) =>
		string.Join("", input.Select((c, i) => c.Transform(i)));

	private static string Transform(this char item, int index) =>
		new(Enumerable.Repeat(index % 2 == 0 ? (char)('0' + index / 2) : '.', item - '0').ToArray());

	private static bool MoveSingle(this char[] list)
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

	private static bool MoveBlock(this char[] list)
	{
		var space = list.SkipWhile(x => x != '.').TakeWhile(x => x == '.');

		Console.WriteLine(new string(list));

		return false;
	}
}