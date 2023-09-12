namespace AdventOfCode2022;
using Extensions;
public static class Day8
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/08/Input.txt")
            .Select(l => l.Chunk(1).Select(c => int.Parse(c)).ToArray()).ToArray();

        int result1 = input.Select((row, x) => row.Select((_, y) => VisibleFromOutside(input, x, y)))
            .SelectMany(v => v).Count(v => v);

        int result2 = input.Select((row, x) => row.Select((_, y) => ScenicScore(input, x, y)))
            .SelectMany(s => s).Max(s => s);

        Console.WriteLine($"Part 1: {result1} (1845), Part 2: {result2} (230112)");

        bool VisibleFromOutside(int[][] forest, int x, int y) =>
            View1(forest, x, y).All(tree => tree < forest[x][y]) ||
            View2(forest, x, y).All(tree => tree < forest[x][y]) ||
            View3(forest, x, y).All(tree => tree < forest[x][y]) ||
            View4(forest, x, y).All(tree => tree < forest[x][y]);

        int ScenicScore(int[][] forest, int x, int y) =>
            View1(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count() *
            View2(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count() *
            View3(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count() *
            View4(forest, x, y).TakeUntil(tree => tree >= forest[x][y]).Count();

        IEnumerable<int> View1(int[][] forest, int x, int y) => Enumerable.Range(0, x)
            .Select(i => forest[i][y]).Reverse();
        IEnumerable<int> View2(int[][] forest, int x, int y) => Enumerable.Range(0, y)
            .Select(i => forest[x][i]).Reverse();
        IEnumerable<int> View3(int[][] forest, int x, int y) => Enumerable.Range(x, forest.Length - x)
            .Select(i => forest[i][y]).Skip(1);
        IEnumerable<int> View4(int[][] forest, int x, int y) => Enumerable.Range(y, forest[0].Length - y)
            .Select(i => forest[x][i]).Skip(1);
    }
}