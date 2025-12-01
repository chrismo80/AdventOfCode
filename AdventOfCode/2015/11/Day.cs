namespace AdventOfCode2015;

public static class Day11
{
	public static IEnumerable<object> Solve(string input)
	{
		var password = input;

		while (!IsValid(password))
			password = NextPassword(password);

		yield return password;

		password = NextPassword(password);

		while (!IsValid(password))
			password = NextPassword(password);

		yield return password;

		string NextPassword(string word, int i = 8)
		{
			var current = word[--i];

			if (current++ < 'z')
				return word[..i] + current + word[(i + 1)..];

			return NextPassword(word[..i] + 'a' + word[(i + 1)..], i);
		}

		bool IsValid(string word)
		{
			return HasNoInvalid(word) && HasIncreasingTriple(word) && HasTwoPairs(word);
		}

		bool HasIncreasingTriple(string word)
		{
			return Enumerable.Range(0, word.Length - 2)
				.Select(i => $"{word[i]}{word[i + 1]}{word[i + 2]}")
				.Any(pair => pair[2] - pair[1] == 1 && pair[1] - pair[0] == 1);
		}

		bool HasNoInvalid(string word)
		{
			return !word.Contains('i') && !word.Contains('o') && !word.Contains('l');
		}

		bool HasTwoPairs(string word)
		{
			return Enumerable.Range(0, word.Length - 1)
				.Select(i => $"{word[i]}{word[i + 1]}").Where(pair => pair[0] == pair[1])
				.Distinct().Count() > 1;
		}
	}
}