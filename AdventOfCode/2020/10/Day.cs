namespace AdventOfCode2020;

using Extensions;

public static class Day10
{
	public static IEnumerable<object> Solve(string input)
	{
		var adapters = File.ReadAllLines("AdventOfCode/2020/10/Input.txt")
			.Select(int.Parse).Order().ToList();

		var jumps = new Dictionary<int, int> { { 1, 0 }, { 2, 0 }, { 3, 1 } };

		jumps[adapters[0]]++;

		for (var i = 0; i < adapters.Count - 1; i++)
			jumps[adapters[i + 1] - adapters[i]]++;

		var combinationsCount = adapters.Prepend(0).ToList().SplitByThreeJoltJumps()
			.Select(oneJoltJumpsSequence => oneJoltJumpsSequence.Combinations().Count(combination =>
				combination.First() == oneJoltJumpsSequence.First() &&
				combination.Last() == oneJoltJumpsSequence.Last() &&
				combination.ToList().IsValid()))
			.Product(count => (long)count);

		yield return jumps[1] * jumps[3];
		yield return combinationsCount;
	}

	public static bool IsValid(this List<int> sequence) =>
		Enumerable.Range(0, sequence.Count - 1).All(i => sequence[i + 1] - sequence[i] <= 3);

	public static IEnumerable<IEnumerable<int>> SplitByThreeJoltJumps(this List<int> adapters)
	{
		var chunk = new List<int>();

		for (var i = 0; i < adapters.Count - 1; i++)
		{
			chunk.Add(adapters[i]);

			if (adapters[i + 1] - adapters[i] >= 3)
			{
				yield return chunk;
				chunk.Clear();
			}
		}

		chunk.Add(adapters.Last());
		yield return chunk;
	}
}