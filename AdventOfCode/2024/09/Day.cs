using Extensions;

namespace AdventOfCode2024;

public static class Day9
{
	public static void Solve()
	{
		var diskmap = Input.Load(2024, 9).GetDiskMap();

		var result1 = diskmap.ToArray().MoveSingle().GetCheckSum();
		var result2 = diskmap.ToArray().MoveBlocks().GetCheckSum();

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static long GetCheckSum(this char[] diskmap) =>
		diskmap.Select((c, i) => c == '.' ? 0 : (long)(i * (c - '0'))).Sum();

	private static string GetDiskMap(this string input) =>
		string.Join("", input.Select((c, i) => c.Transform(i)));

	private static string Transform(this char item, int index) =>
		new(Enumerable.Repeat(index % 2 == 0 ? (char)('0' + index / 2) : '.', item - '0').ToArray());

	private static char[] MoveSingle(this char[] list)
	{
		int left = 0, right = list.Length;

		while (left < right--)
		{
			if (list[right] == '.')
				continue;

			while (list[left] != '.')
				left++;

			list.Move(left, right);
		}

		return list;
	}

	private static void Move(this char[] list, int left, int right)
	{
		if (left > right)
			return;

		list[left] = list[right];
		list[right] = '.';
	}

	private static char[] MoveBlocks(this char[] list)
	{
		var right = list.Length - 1;

		while (right > 0)
		{
			if (list[right] == '.')
			{
				right--;
				continue;
			}

			var length = list.FindBlock(right);

			if (length == -1)
				break;

			var left = list.FindSpace(0, length);

			if (left == -1)
			{
				right -= length;
				continue;
			}

			while (length-- > 0)
				list.Move(left++, right--);
		}

		return list;
	}

	private static int FindBlock(this char[] list, int start)
	{
		var current = start;
		var value = list[current];

		while (current > 0 && list[current] == value)
			current--;

		if (current <= 0)
			return -1;

		return start - current;
	}

	private static int FindSpace(this char[] list, int start, int length)
	{
		var current = start;

		while (length > current - start)
		{
			while (current < list.Length - 1 && list[current] != '.')
				current++;

			start = current;

			while (current < list.Length - 1 && list[current] == '.')
				current++;

			if (current >= list.Length - 1)
				return -1;
		}

		return start;
	}
}