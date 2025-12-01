namespace AdventOfCode2015;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		yield return input.Count(c => c == '(') - input.Count(c => c == ')');

		yield return Enumerable.Range(0, input.Length)
			.First(i => input.Take(i).Count(c => c == '(') - input.Take(i).Count(c => c == ')') < 0);
	}
}