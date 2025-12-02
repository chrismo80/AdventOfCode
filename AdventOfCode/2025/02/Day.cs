using AdventOfCode;

namespace AdventOfCode2025;

public static class Day2
{
	public static IEnumerable<object> Solve(string input)
	{
		var ranges = input.ToNestedArray<long>(",", "-");

		yield return ranges.Sum(range => GetInvalid(range.First(), range.Last()).Sum());
	}

	private static IEnumerable<long> GetInvalid(long start, long end)
	{
		for (var i = start; i <= end; i++)
		{
			if (i < 11)
				continue;

			var text = i.ToString();

			for (var l = 1; l <= text.Length; l++)
			{
				var chunkSize = text.Length / l;

				var chunks = text.Chunk(chunkSize).Select(chars => new string(chars));

				if (chunks.Count() > 1 && chunks.All(chunk => chunk == chunks.First()))
				{
					yield return i;
					break;
				}
			}
		}
	}
}