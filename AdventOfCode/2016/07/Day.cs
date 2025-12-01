using AdventOfCode;

namespace AdventOfCode2016;

public static class Day7
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(row => row.Split('[', ']'));

		yield return data.Count(ip =>
			ip.Where((_, i) => i % 2 != 0).All(part => !GetAllABBA(part).Any()) &&
			ip.Where((_, i) => i % 2 == 0).Any(part => GetAllABBA(part).Any()));

		yield return data.Count(ip => ip
			.Where((_, i) => i % 2 != 0).Select(part => GetAllABA(part)).SelectMany(aba => aba)
			.Intersect(ip.Where((_, i) => i % 2 == 0)
				.Select(part => GetAllABA(part).Select(aba => InverseABA(aba))).SelectMany(bab => bab)
			).Any());

		string[] GetAllABBA(string part)
		{
			return Enumerable.Range(-1, part.Length - 3)
				.Where(i => part[i + 1] != part[i + 2] && part[i + 1] == part[i + 4] && part[i + 2] == part[i + 3])
				.Select(i => part.Substring(i + 1, 4)).ToArray();
		}

		string[] GetAllABA(string part)
		{
			return Enumerable.Range(-1, part.Length - 2)
				.Where(i => part[i + 1] != part[i + 2] && part[i + 1] == part[i + 3])
				.Select(i => part.Substring(i + 1, 3)).ToArray();
		}

		string InverseABA(string part)
		{
			return new string(part.Append(part[1]).Skip(1).ToArray());
		}
	}
}