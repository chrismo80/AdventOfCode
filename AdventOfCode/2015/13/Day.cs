using AdventOfCode;

namespace AdventOfCode2015;

using Extensions;

public static class Day13
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines()
			.Select(row => row.Split(' '))
			.Select(words => (Name: words[0][0], Neighbor: words[10][0],
				Happiness: int.Parse(words[3]) * (words[2] == "gain" ? 1 : -1)))
			.ToList();

		yield return string.Concat(data.Select(p => p.Name).Distinct()).Where(c => c != 'A')
			.Permutations().Select(p => string.Concat(p) + 'A').Max(v => v.Sum(p => Happiness(p, v)));

		yield return string.Concat(data.Select(p => p.Name).Distinct())
			.Permutations().Select(p => string.Concat(p) + 'X').Max(v => v.Sum(p => Happiness(p, v)));

		int Happiness(char name, IEnumerable<char> permutation)
		{
			return data
				.Where(p => p.Name == name && permutation.Neighbors(name).Contains(p.Neighbor))
				.Sum(p => p.Happiness);
		}
	}

	private static IEnumerable<char> Neighbors(this IEnumerable<char> source, char element)
	{
		var pos = source.ToList().IndexOf(element);

		yield return source.ElementAt((pos + 1) % source.Count());
		yield return source.ElementAt((pos - 1 + source.Count()) % source.Count());
	}
}