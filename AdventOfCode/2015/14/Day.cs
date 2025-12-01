namespace AdventOfCode2015;

public static class Day14
{
	public static IEnumerable<object> Solve(string input)
	{
		var reindeers = File.ReadAllLines("AdventOfCode/2015/14/Input.txt")
			.Select(row => row.Split(' ').Where(word => int.TryParse(word, out var value)).Select(int.Parse).ToArray())
			.Select(words => (Speed: words[0], Flying: words[1], Resting: words[2]));

		const int duration = 2503;

		var distances = reindeers.Select(reindeer => Enumerable.Range(1, duration)
			.Select(seconds => Distance(seconds, reindeer)));

		var points = Enumerable.Range(1, duration)
			.Select(seconds => reindeers.Select(reindeer => Distance(seconds, reindeer)))
			.Select(seconds => seconds.Select(distance => distance == seconds.Max() ? 1 : 0));

		yield return distances.Max(distances => distances.Last());
		yield return reindeers.Select((_, i) => points.Sum(reindeer => reindeer.ElementAt(i))).Max();

		static int Distance(int seconds, (int Speed, int Flying, int Resting) r)
		{
			return seconds / (r.Flying + r.Resting) * r.Speed * r.Flying +
				Math.Min(seconds % (r.Flying + r.Resting), r.Flying) * r.Speed;
		}
	}
}