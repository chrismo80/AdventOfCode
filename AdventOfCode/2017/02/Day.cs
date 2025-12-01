using AdventOfCode;

namespace AdventOfCode2017;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var numbers = input.Lines()
			.Select(row => row.Split('\t').Select(int.Parse));

		yield return numbers.Sum(row => row.Max() - row.Min());
		yield return numbers.Select(row => FindNumbers(row)).Sum(n => n.X / n.Y);

		static (int X, int Y) FindNumbers(IEnumerable<int> row)
		{
			return (from x in row from y in row where x != y && x % y == 0 select (x, y)).Single();
		}
	}
}