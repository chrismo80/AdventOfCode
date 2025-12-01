using AdventOfCode;

namespace AdventOfCode2022;

public static class Day20
{
	public static IEnumerable<object> Solve(string input)
	{
		var numbers = input.ToArray<int>("\n").ToList();

		var data = numbers.ToList();

		for (var i = 0; i < numbers.Count; i++)
		{
			//Console.WriteLine(string.Join(',', data));

			var value = numbers[i];
			var position = data.IndexOf(value);

			data.RemoveAt(position);
			data.Insert(mod(position + value, numbers.Count - 1), value);
		}

		yield return Enumerable.Range(1, 3).Sum(i => data[(data.IndexOf(0) + i * 1000) % data.Count]);

		static int mod(int x, int m)
		{
			return (x % m + m) % m;
		}
	}
}