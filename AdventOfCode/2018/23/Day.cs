using AdventOfCode;

namespace AdventOfCode2018;

public static class Day23
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.ToArray<string>("\n")
			.Select(row => row.Split('<', '>', ',', '='))
			.Select(items =>
			(
				Pos: items.Skip(2).Take(3).Select(long.Parse).ToArray(),
				Radius: long.Parse(items.Last())
			))
			.ToArray();

		var strongest = data.OrderBy(robot => robot.Radius).Last();

		yield return data.Count(robot => robot.Pos.Zip(strongest.Pos, (r, s) => Math.Abs(r - s)).Sum() <= strongest.Radius);

		var bestRobot = data
			.OrderByDescending(robot => RobotsInRange(robot.Pos))
			.ThenBy(robot => robot.Pos.Sum());

		var bestRanges = bestRobot.Select(robot => RobotsInRange(robot.Pos));
		var robotsNearBy = RobotsInRange(bestRobot.First().Pos);

		int RobotsInRange(long[] pos)
		{
			return data.Count(robot => robot.Pos.Zip(pos, (i, j) => Math.Abs(i - j)).Sum() <= robot.Radius);
		}
	}
}