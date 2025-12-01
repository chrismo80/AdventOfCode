using AdventOfCode;

namespace AdventOfCode2021;

public static class Day4
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		var numbersDrawn = lines[0].Split(',').Select(s => int.Parse(s));

		var tables = lines.Skip(1).Chunk(6)
			.Select(table => table.Skip(1)
				.Select(row => row.Chunk(3)
					.Select(c => int.Parse(string.Concat(c).Trim())))
			);

		var finishedTables = new List<int>();
		var finishedNumbers = new List<int>();

		for (var n = 1; n <= numbersDrawn.Count(); n++)
		for (var t = 0; t < tables.Count(); t++)
			if (!finishedTables.Contains(t) && tables.ElementAt(t).Won(numbersDrawn.Take(n)) > 0)
			{
				finishedTables.Add(t);
				finishedNumbers.Add(n);
			}

		yield return tables.ElementAt(finishedTables.First()).Won(numbersDrawn.Take(finishedNumbers.First()));
		yield return tables.ElementAt(finishedTables.Last()).Won(numbersDrawn.Take(finishedNumbers.Last()));
	}

	private static int Won(this IEnumerable<IEnumerable<int>> table, IEnumerable<int> numbers)
	{
		var rowStats = table
			.Select(row => row.Intersect(numbers).Count());

		var colStats = table
			.SelectMany(inner => inner.Select((item, index) => (item, index)))
			.GroupBy(i => i.index, i => i.item)
			.Select(col => col.Intersect(numbers).Count());

		if (!(rowStats.Any(item => item == 5) || colStats.Any(item => item == 5)))
			return 0;

		var all = table.Sum(row => row.Sum());
		var marked = table.Sum(row => row.Intersect(numbers).Sum());

		return (all - marked) * numbers.Last();
	}
}