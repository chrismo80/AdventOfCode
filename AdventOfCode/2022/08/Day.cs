using AdventOfCode;

namespace AdventOfCode2022;

using Extensions;

public static class Day8
{
	public static IEnumerable<object> Solve(string input)
	{
		var map = input.Lines()
			.Select(l => l.Chunk(1).Select(c => int.Parse(c)).ToArray()).ToArray();

		yield return map.Select((row, x) => row.Select((_, y) => VisibleFromOutside(map, x, y)))
			.SelectMany(v => v).Count(v => v);

		yield return map.Select((row, x) => row.Select((_, y) => ScenicScore(map, x, y)))
			.SelectMany(s => s).Max(s => s);

		bool VisibleFromOutside(int[][] forest, int x, int y)
		{
			return View1(forest, x, y).All(tree => tree < forest[x][y]) ||
				View2(forest, x, y).All(tree => tree < forest[x][y]) ||
				View3(forest, x, y).All(tree => tree < forest[x][y]) ||
				View4(forest, x, y).All(tree => tree < forest[x][y]);
		}

		int ScenicScore(int[][] forest, int x, int y)
		{
			return View1(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count() *
				View2(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count() *
				View3(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count() *
				View4(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count();
		}

		IEnumerable<int> View1(int[][] forest, int x, int y)
		{
			return Enumerable.Range(0, x)
				.Select(i => forest[i][y]).Reverse();
		}

		IEnumerable<int> View2(int[][] forest, int x, int y)
		{
			return Enumerable.Range(0, y)
				.Select(i => forest[x][i]).Reverse();
		}

		IEnumerable<int> View3(int[][] forest, int x, int y)
		{
			return Enumerable.Range(x, forest.Length - x)
				.Select(i => forest[i][y]).Skip(1);
		}

		IEnumerable<int> View4(int[][] forest, int x, int y)
		{
			return Enumerable.Range(y, forest[0].Length - y)
				.Select(i => forest[x][i]).Skip(1);
		}
	}
}