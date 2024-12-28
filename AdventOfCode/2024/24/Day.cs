using AdventOfCode;

namespace AdventOfCode2024;

public static class Day24
{
	public static void Solve()
	{
		var input = Input.Load(2024, 24).ToArray<string>("\n\n");

		var values = input.First().ToNestedArray<string>("\n", ": ")
			.ToDictionary(kvp => kvp[0], kvp => int.Parse(kvp[1]));

		var connections = input.Last().ToNestedArray<string>("\n", " ");

		while (connections.FirstOrDefault(c => !values.ContainsKey(c.Last())) != null)
			foreach (var connection in connections)
			{
				if (values.ContainsKey(connection.Last()))
					continue;

				if (!values.ContainsKey(connection[0]) || !values.ContainsKey(connection[2]))
					continue;

				values[connection.Last()] = Connect(values[connection[0]], connection[1], values[connection[2]]);
			}

		var zValues = values.Where(kvp => kvp.Key.StartsWith("z")).OrderByDescending(kvp => kvp.Key)
			.Select(kvp => kvp.Value);

		var result1 = Convert.ToInt64(string.Join("", zValues), 2);
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static int Connect(int left, string gate, int right) => gate switch
	{
		"AND" => left & right, "OR" => left | right, "XOR" => left ^ right
	};
}