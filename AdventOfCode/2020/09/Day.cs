using AdventOfCode;

namespace AdventOfCode2020;

public static class Day9
{
	public static IEnumerable<object> Solve(string input)
	{
		var numbers = input.ToArray<long>("\n");

		var result1 = FindInvalid(numbers, 25);

		yield return result1;
		yield return SetMatch(numbers.Skip(FindSet(numbers, result1)).ToArray(), result1);

		long FindInvalid(IEnumerable<long> stream, int preamble)
		{
			return numbers.ElementAt
			(Enumerable.Range(preamble, stream.Count())
				.First(i => !IsValid(stream.Skip(i - preamble).Take(preamble), stream.ElementAt(i))));
		}

		bool IsValid(IEnumerable<long> preamble, long number)
		{
			return (from x in preamble from y in preamble where x + y == number select (x, y)).Any();
		}

		int FindSet(IEnumerable<long> stream, long expectedSum)
		{
			return Enumerable.Range(0, stream.Count())
				.First(i => SetMatch(stream.Skip(i).ToArray(), expectedSum) > 0);
		}

		long SetMatch(long[] stream, long expectedSum)
		{
			long sum = 0;

			for (var i = 0; i < stream.Length; i++)
			{
				sum += stream[i];

				if (sum == expectedSum)
					return stream.Take(i + 1).Min() + stream.Take(i + 1).Max();
			}

			return 0;
		}
	}
}