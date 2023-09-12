namespace AdventOfCode2015;
public static class Day1
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2015/01/Input.txt")[0];

        var result1 = input.Count(c => c == '(') - input.Count(c => c == ')');
        var result2 = Enumerable.Range(0, input.Length)
            .First(i => input.Take(i).Count(c => c == '(') - input.Take(i).Count(c => c == ')') < 0);

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }
}