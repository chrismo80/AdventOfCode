namespace AdventOfCode2019;

public static class Day4
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Split('-').Select(int.Parse);

		var passwords = Enumerable.Range(data.First(), data.Last() - data.First() + 1)
			.Select(n => n.ToString());

		var result1 = passwords
			.Where(p => p.SequenceEqual(p.Order()) && p.GroupBy(c => c).Any(group => group.Count() > 1));

		var result2 = passwords
			.Where(p => p.SequenceEqual(p.Order()) && p.GroupBy(c => c).Any(group => group.Count() == 2));

		yield return result1.Count();
		yield return result2.Count();
	}
}