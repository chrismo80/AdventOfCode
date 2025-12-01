namespace AdventOfCode2022;

using System.Text;
using System.Text.RegularExpressions;

public static partial class Day5
{
	[GeneratedRegex("move (\\d+) from (\\d+) to (\\d+)")]
	private static partial Regex Instruction();

	public static IEnumerable<object> Solve(string input)
	{
		//Solve2(File.ReadAllLines("AdventOfCode/2022/05/Test.txt"));
		var parts = input.Split("\n\n");

		var stacks = parts[0]
			.Split("\n").Select(line => line.Chunk(4).Select(x => x[1]))
			.SelectMany(inner => inner.Select((item, index) => new { item, index }))
			.GroupBy(i => i.index, i => i.item)
			.Select(g => g.Reverse().Where(c => c != ' ').Skip(1)).ToArray();

		var instructions = parts[1].Split("\n").Select(line => (
			Amount: int.Parse(Instruction().Match(line).Groups[1].Value),
			From: int.Parse(Instruction().Match(line).Groups[2].Value) - 1,
			To: int.Parse(Instruction().Match(line).Groups[3].Value) - 1));

		yield return CrateMover(9000, stacks, instructions);
		yield return CrateMover(9001, stacks, instructions);
	}

	private static string CrateMover(int mover, IEnumerable<char>[] origStacks,
		IEnumerable<(int Amount, int From, int To)> instructions)
	{
		var stacks = new IEnumerable<char>[origStacks.Length];
		origStacks.CopyTo(stacks, 0);

		foreach (var (Amount, From, To) in instructions)
			switch (mover)
			{
				case 9000:
					for (var i = 0; i < Amount; i++) MoveCrates(stacks, 1, From, To);
					break;
				case 9001: MoveCrates(stacks, Amount, From, To); break;
			}

		return new string(stacks.Select(s => s.Last()).ToArray());
	}

	private static void MoveCrates(IEnumerable<char>[] stacks, int amount, int from, int to)
	{
		stacks[to] = stacks[to].Concat(stacks[from].TakeLast(amount)).ToArray();
		stacks[from] = stacks[from].SkipLast(amount).ToArray();
	}

	public static void Solve2(string[] lines)
	{
		var stacks = new List<StringBuilder>();

		foreach (var line in lines)
		{
			if (line.StartsWith('['))
			{
				var crates = line.Chunk(4).ToArray();
				for (var i = 0; i < crates.Length; i++)
				{
					if (stacks.Count < i + 1)
						stacks.Add(new StringBuilder());

					if (crates[i][0] == '[')
						stacks[i].Append(crates[i][1]);
				}
			}

			if (line.StartsWith("move"))
			{
				var m = line.Split(new string[] { "move", "from", "to" }, StringSplitOptions.RemoveEmptyEntries);

				var (count, from, to) = (int.Parse(m[0]), int.Parse(m[1]), int.Parse(m[2]));
				var sb = new StringBuilder();

				for (var i = 0; i < count; i++)
					sb.Append(stacks[from - 1][i]);

				stacks[from - 1].Remove(0, count);
				stacks[to - 1].Insert(0, sb.ToString());
			}
		}

		Console.WriteLine(new string(stacks.Select(s => s[0]).ToArray()));
	}
}