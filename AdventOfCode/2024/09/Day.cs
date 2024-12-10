namespace AdventOfCode2024;

public static class Day9
{
	public static void Solve()
	{
		var diskmap = File.ReadAllText("AdventOfCode/2024/09/Input.txt")
			.GetDiskMap();

		var result1 = diskmap.ToArray().MoveSingle().GetCheckSum();
		var result2 = diskmap.ToArray().MoveBlock().GetCheckSum();

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static long GetCheckSum(this char[] diskmap) =>
		diskmap.Where(x => x != '.').Select((c, i) => (long)(i * (c - '0'))).Sum();

	private static string GetDiskMap(this string input) =>
		string.Join("", input.Select((c, i) => c.Transform(i)));

	private static string Transform(this char item, int index) =>
		new(Enumerable.Repeat(index % 2 == 0 ? (char)('0' + index / 2) : '.', item - '0').ToArray());

	private static char[] MoveSingle(this char[] list)
	{
		var index = list.Length - 1;

		for (var i = 0; i < list.Length; i++)
		{
			if (list[i] != '.')
				continue;

			while (list[index] == '.')
				index--;

			list[i] = list[index];
			list[index] = '.';

			if (i > index)
				break;
		}

		return list;
	}

	private static char[] MoveBlock(this char[] list) => list;
}