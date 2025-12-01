namespace AdventOfCode2018;

public static class Day5
{
	public static IEnumerable<object> Solve(string input)
	{
		yield return React(input);

		yield return Enumerable.Range('a', 26).Select(c => $"{(char)c}")
			.Select(unit => React(input.Replace(unit, "").Replace(unit.ToUpper(), "")))
			.Min();

		static int React(string polymer)
		{
			var reactions =
				Enumerable.Range('a', 26).Zip(Enumerable.Range('A', 26),
					(l, u) => $"{(char)l}{(char)u}").Concat(
					Enumerable.Range('A', 26).Zip(Enumerable.Range('a', 26),
						(u, l) => $"{(char)u}{(char)l}"));

			var length = 0;

			while (length != polymer.Length)
			{
				length = polymer.Length;

				foreach (var reaction in reactions)
					polymer = polymer.Replace(reaction, "");
			}

			return polymer.Length;
		}
	}
}