namespace AdventOfCode2015;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var houses1 = new List<(int X, int Y)> { (0, 0) };
		var houses2 = new List<(int X, int Y)> { (0, 0) };

		static (int, int) NewHouse((int X, int Y) lastHouse, char direction)
		{
			return (
				lastHouse.X + (direction == 'v' ? 1 : direction == '^' ? -1 : 0),
				lastHouse.Y + (direction == '>' ? 1 : direction == '<' ? -1 : 0));
		}

		foreach (var move in input)
			houses1.Add(NewHouse(houses1.Last(), move));

		foreach (var move in input.Where((_, i) => i % 2 == 0))
			houses2.Add(NewHouse(houses2.Last(), move));

		houses2.Add((0, 0));

		foreach (var move in input.Where((_, i) => i % 2 != 0))
			houses2.Add(NewHouse(houses2.Last(), move));

		yield return houses1.Distinct().Count();
		yield return houses2.Distinct().Count();
	}
}