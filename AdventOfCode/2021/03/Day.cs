using AdventOfCode;

namespace AdventOfCode2021;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		var data = lines
			.SelectMany(inner => inner.Select((item, index) => (item, index)))
			.GroupBy(i => i.index, i => i.item)
			.Select(g => g.Count(c => c == '1') > lines.Length / 2 ? '1' : '0');

		var gamma = Convert.ToInt32(string.Concat(data), 2);
		var epsilon = ~gamma & 0xFFF;

		yield return gamma * epsilon;

		yield return lines.SelectMany(inner => inner.Select((item, index) => (item, index)))
			.GroupBy(i => i.index, i => i.item).First();
	}
}