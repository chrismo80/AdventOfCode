using AdventOfCode;

namespace AdventOfCode2025;

public static class Day4
{
	public static IEnumerable<object> Solve(string input)
	{
		var map = input.ToMap();

		var rolls = map.Find('@').ToList();

		bool IsReachable((int X, int Y) pos)
		{
			return pos.Neighbors(map[0].Length, map.Length, true).Count(n => rolls.Contains(n)) < 4;
		}

		yield return rolls.Count(IsReachable);

		var initialCount = rolls.Count;

		while (rolls.Any(IsReachable))
			rolls.RemoveAll(IsReachable);

		yield return initialCount - rolls.Count;
	}
}