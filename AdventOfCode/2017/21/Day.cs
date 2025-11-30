namespace AdventOfCode2017;

public static class Day21
{
	public static void Solve()
	{
		var input = File.ReadAllLines("AdventOfCode/2017/21/Test.txt")
			.Select(row => row.Split(" => ")).ToArray();

		var rules = new Dictionary<string, string>();

		foreach (var rule in input)
		{
			rules[rule[0]] = rule[1];
			rules[rule[0].FlipH()] = rule[1];
			rules[rule[0].FlipV()] = rule[1];
			rules[rule[0].FlipH().FlipV()] = rule[1];
			rules[rule[0].FlipH().FlipV().Rot90()] = rule[1];
			rules[rule[0].Rot90()] = rule[1];
			rules[rule[0].FlipH().FlipV().Rot90().FlipH()] = rule[1];
			rules[rule[0].Rot90().FlipH()] = rule[1];
		}

		var image = ".#./..#/###";

		var image2 = "abcd/defg/hijk/lmno";
		var image3 = "ab/de / cd/fg / hi/lm / jk/no";

		var iterations = 2;

		while (iterations-- > 0)
			if (image.Split('/')[0].Length % 2 == 0)
			{ }
			else if (image.Split('/')[0].Length % 3 == 0)
			{
				image = rules[image];
			}

		Console.WriteLine($"Part 1: {0}, Part 2: {0}");
	}

	private static string FlipH(this string pattern) =>
		string.Join('/', pattern.Split('/').Select(p => new string(p.Reverse().ToArray())));

	private static string FlipV(this string pattern) =>
		string.Join('/', pattern.Split('/').AsEnumerable().Reverse());

	private static string Rot90(this string pattern)
	{
		var grid = pattern.Split('/').Select(c => c.ToArray()).ToArray();
		var rot = pattern.Split('/').Select(c => c.ToArray()).ToArray();

		for (var i = 0; i < grid.Length; ++i)
		for (var j = 0; j < grid.Length; ++j)
			rot[i][j] = grid[grid.Length - j - 1][i];
		return string.Join('/', rot.Select(row => new string(row)));
	}
}