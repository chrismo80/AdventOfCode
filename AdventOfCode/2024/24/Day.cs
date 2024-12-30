using AdventOfCode;

namespace AdventOfCode2024;

public static class Day24
{
	public static void Solve()
	{
		var input = Input.Load(2024, 24).ToArray<string>("\n\n");

		var initialValues = input.First().ToNestedArray<string>("\n", ": ")
			.ToDictionary(kvp => kvp[0], kvp => int.Parse(kvp[1]));

		var initialWires = input.Last().ToNestedArray<string>("\n", " ");

		var dict = initialValues.Calculate(initialWires);

		for (var i = 0; i < 45; i++)
			Console.WriteLine($"{i:00}: " + dict[$"x{i:00}"] + " + " + dict[$"y{i:00}"] + " = " + dict[$"z{i:00}"]);

		var expected = initialValues.Value('x') + initialValues.Value('y');

		var result1 = initialValues.Calculate(initialWires).Value('z');
		var result2 = 0;

		Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
	}

	private static bool Check(this IEnumerable<string> inputs, string output)
	{
		var bit = int.Parse(output[1..]);

		if (inputs.Contains($"x{bit:00}") && inputs.Contains($"y{bit:00}"))
			return true;

		var distinct = inputs.Distinct().Order().ToArray();

		return false;
	}

	private static IEnumerable<string> FindInputs(this string[][] wires, string output)
	{
		var connection = wires.FirstOrDefault(wire => wire.Last() == output);

		if (connection == null)
			yield break;

		if (connection[0][0] == 'x' || connection[0][0] == 'y')
			yield return connection[0];

		foreach (var input in wires.FindInputs(connection[0]))
			yield return input;

		if (connection[2][0] == 'x' || connection[2][0] == 'y')
			yield return connection[2];

		foreach (var input in wires.FindInputs(connection[2]))
			yield return input;
	}

	private static string[][] Copy(this string[][] array) =>
		array.Select(items => items.ToArray()).ToArray();

	private static string[][] Swap(this string[][] wires, int from, int to)
	{
		var first = wires[from].SkipLast(1).Append(wires[to].Last()).ToArray();
		var second = wires[to].SkipLast(1).Append(wires[from].Last()).ToArray();

		wires[from] = first;
		wires[to] = second;

		return wires;
	}

	private static Dictionary<string, int> Calculate(this Dictionary<string, int> initialValues, string[][] wires)
	{
		var dict = initialValues.ToDictionary(entry => entry.Key, entry => entry.Value);

		var count = 0;

		while (wires.Any(w => !dict.ContainsKey(w.Last())) && dict.Count > count)
		{
			count = dict.Count;

			foreach (var wire in wires.Where(w =>
						!dict.ContainsKey(w.Last()) && dict.ContainsKey(w[0]) && dict.ContainsKey(w[2])))
				dict[wire.Last()] = Connect(dict[wire[0]], wire[1], dict[wire[2]]);
		}

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