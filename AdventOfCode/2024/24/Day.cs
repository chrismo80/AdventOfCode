using AdventOfCode;

namespace AdventOfCode2024;

public static class Day24
{
	public static void Solve()
	{
		var input = Input.Load(2024, 24).ToArray<string>("\n\n");

		var initialValues = input.First().ToNestedArray<string>("\n", ": ")
			.ToDictionary(kvp => kvp[0], kvp => int.Parse(kvp[1]));

		var connections = input.Last().ToNestedArray<string>("\n", " ");

		var expected = initialValues.Value('x') + initialValues.Value('y');

		var result1 = initialValues.Calculate(connections).Value('z');
		var result2 = expected;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static Dictionary<string, int> Calculate(this Dictionary<string, int> initialValues, string[][] connections)
	{
		var dict = initialValues.ToDictionary(entry => entry.Key, entry => entry.Value);

		while (connections.Any(c => !dict.ContainsKey(c.Last())))
			foreach (var connection in connections.Where(c =>
						!dict.ContainsKey(c.Last()) && dict.ContainsKey(c[0]) && dict.ContainsKey(c[2])))
				dict[connection.Last()] = Connect(dict[connection[0]], connection[1], dict[connection[2]]);

		return dict;
	}

	private static long Value(this Dictionary<string, int> dict, char start)
	{
		var filtered = dict.Where(entry => entry.Key.StartsWith(start))
			.OrderByDescending(entry => entry.Key)
			.Select(entry => entry.Value);

		return Convert.ToInt64(string.Join("", filtered), 2);
	}

	private static int Connect(int left, string gate, int right) => gate switch
	{
		"AND" => left & right, "OR" => left | right, "XOR" => left ^ right
	};
}