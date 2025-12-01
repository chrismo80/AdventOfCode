using AdventOfCode;

namespace AdventOfCode2017;

public static class Day13
{
	public static IEnumerable<object> Solve(string input)
	{
		var scanners = input.Lines()
			.Select(row => row.Split(": ").Select(int.Parse))
			.Select(data => new Scanner { Depth = data.First(), Range = data.Last() })
			.ToArray();

		yield return scanners.Where(scanner => scanner.WillGetYou).Sum(s => s.Depth * s.Range);
		yield return Enumerable.Range(0, int.MaxValue)
			.First(delay => scanners.All(s => (s.Depth + delay) % (2 * (s.Range - 1)) != 0));

		//while (scanners.Any(scanner => scanner.WillGetYou) && result2++ < int.MaxValue)
		//    foreach (var scanner in scanners) scanner.MoveStep();
	}

	public class Scanner
	{
		public int Depth, Range, Pos;
		public int Cycle => 2 * (Range - 1);
		public bool WillGetYou => (Cycle - Pos) % Cycle == Depth % Cycle;

		public void MoveStep()
		{
			Pos = (Pos + 1) % Cycle;
		}
	}
}