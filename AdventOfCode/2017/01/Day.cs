namespace AdventOfCode2017;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		yield return FindMatches(input.Concat(input), 1);
		yield return FindMatches(input.Concat(input), input.Length / 2);

		static int FindMatches(IEnumerable<char> stream, int distance)
		{
			return Enumerable.Range(0, stream.Count() / 2)
				.Where(i => stream.Skip(i).First() == stream.Skip(i + distance).First())
				.Sum(i => stream.ElementAt(i) - '0');
		}
	}
}