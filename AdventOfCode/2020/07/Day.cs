using AdventOfCode;

namespace AdventOfCode2020;

public static class Day7
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(text => text.Split(" bag").Where(t => !t.Contains("no other")).SkipLast(1)
				.Select(words => string.Join(' ', words.Split(' ').TakeLast(3))).ToArray())
			.Select(bags => (Outer: bags[0], Inner: bags.Skip(1).ToList())).ToList();

		yield return CountOuterBags("shiny gold", data);
		yield return CountInnerBags("shiny gold", data) - 1;

		static int CountOuterBags(string bagColor, List<(string Outer, List<string> Inner)> data)
		{
			var outerBags = new HashSet<string>();
			var counter = -1;

			while (counter != outerBags.Count)
			{
				counter = outerBags.Count;

				foreach (var (Outer, Inner) in data.Where(bag => !outerBags.Contains(bag.Outer)))
				{
					var innerBags = Inner.Select(words => string.Join(' ', words.Split(' ').Skip(1)));

					if (innerBags.Contains(bagColor) || innerBags.Intersect(outerBags).Any())
						outerBags.Add(Outer);
				}
			}

			return outerBags.Count;
		}

		static int CountInnerBags(string bagColor, List<(string Outer, List<string> Inner)> data)
		{
			var counter = 1;

			foreach (var bag in data.First(bag => bag.Outer == bagColor).Inner)
			{
				var color = string.Join(' ', bag.Split(' ').Skip(1));
				counter += int.Parse(bag.Split(' ')[0]) * CountInnerBags(color, data);
			}

			return counter;
		}

		static int CountInnerBagsLINQ(string bagColor, List<(string Outer, List<string> Inner)> data)
		{
			return data.First(b => b.Outer == bagColor).Inner.Sum
			(bag =>
				int.Parse(bag.Split(' ')[0]) *
				CountInnerBagsLINQ(string.Join(' ', bag.Split(' ').Skip(1)), data)
			) + 1;
		}
	}
}