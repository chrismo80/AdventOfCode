using AdventOfCode;

namespace AdventOfCode2025;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<int>("\n");

		yield return data.Sum();
	}
}

public static class EnumerableExtensions
{
	extension<T>(IEnumerable<T> target)
	{
		public bool IsEmpty()
		{
			return !target.Any();
		}
	}
}