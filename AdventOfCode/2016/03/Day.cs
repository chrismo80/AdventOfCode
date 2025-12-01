using AdventOfCode;

namespace AdventOfCode2016;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(row => row.Split(" ", StringSplitOptions.RemoveEmptyEntries)
			.Select(int.Parse).ToArray());

		var result2 = data.Select(g => g[0])
			.Concat(data.Select(g => g[1]))
			.Concat(data.Select(g => g[2]))
			.Chunk(3);

		yield return data.Count(sides => IsTriangle(sides));
		yield return result2.Count(sides => IsTriangle(sides));

		static bool IsTriangle(int[] s)
		{
			return s[0] + s[1] > s[2] && s[0] + s[2] > s[1] && s[1] + s[2] > s[0];
		}
	}
}