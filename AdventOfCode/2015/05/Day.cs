using AdventOfCode;

namespace AdventOfCode2015;

public static class Day5
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		yield return lines.Count(word => HasNoInvalid(word) && HasCharsInARow(word) && Has3Vowels(word));
		yield return lines.Count(word => ContainsRepeatedInBetween(word) && ContainsMultiplePairs(word));

		bool ContainsRepeatedInBetween(string word)
		{
			return word.SkipWhile((_, i) =>
				!word.Skip(i).Take(1).SequenceEqual(word.Skip(i + 2).Take(1))
			).Any();
		}

		bool ContainsMultiplePairs(string word)
		{
			return word.SkipWhile((_, i) =>
				!new string(word.Skip(i + 2).ToArray()).Contains(new string(word.Skip(i).Take(2).ToArray()))
			).Any();
		}

		bool HasNoInvalid(string word)
		{
			return !word.Contains("ab") &&
				!word.Contains("cd") &&
				!word.Contains("pq") &&
				!word.Contains("xy");
		}

		bool HasCharsInARow(string word)
		{
			return word.SkipWhile((_, i) =>
				!word.Skip(i).Take(1).SequenceEqual(word.Skip(i + 1).Take(1))
			).Any();
		}

		bool Has3Vowels(string word)
		{
			return word.Count(c => c == 'a') +
				word.Count(c => c == 'e') +
				word.Count(c => c == 'i') +
				word.Count(c => c == 'o') +
				word.Count(c => c == 'u') >= 3;
		}
	}
}