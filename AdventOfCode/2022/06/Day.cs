namespace AdventOfCode2022;

public static class Day6
{
	public static IEnumerable<object> Solve(string input)
	{
		int FindMarker1(string stream, int size)
		{
			return Enumerable.Range(size, stream.Length)
				.First(i => stream.Skip(i - size).Take(size).Distinct().Count() == size);
		}

		int FindMarker2(string stream, int size)
		{
			return stream
				.TakeWhile((_, i) => stream.Skip(i).Take(size).Distinct().Count() < size)
				.Count() + size;
		}

		yield return FindMarker1(input, 4);
		yield return FindMarker1(input, 14);
	}
}