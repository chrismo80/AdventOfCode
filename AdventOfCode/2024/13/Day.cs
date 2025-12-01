using System.Text.RegularExpressions;
using AdventOfCode;

namespace AdventOfCode2024;

public static class Day13
{
	public static IEnumerable<object> Solve(string input)
	{
		var clawMachines = input.Split("\n\n")
			.Select(input => new ClawMachine(input.Split('\n')))
			.ToArray();

		yield return clawMachines.Select(m => m.CheapestTokens()).Sum();
	}

	private class ClawMachine
	{
		public (long X, long Y) ButtonA { get; set; }
		public (long X, long Y) ButtonB { get; set; }
		public (long X, long Y) Prize { get; set; }

		public ClawMachine(string[] input)
		{
			var regex = new Regex(@"(\d+)");

			var a = regex.Matches(input[0]);
			var b = regex.Matches(input[1]);
			var p = regex.Matches(input[2]);

			ButtonA = (long.Parse(a[0].Value), long.Parse(a[1].Value));
			ButtonB = (long.Parse(b[0].Value), long.Parse(b[1].Value));
			Prize = (long.Parse(p[0].Value), long.Parse(p[1].Value));
		}

		public long CheapestTokens()
		{
			var routes = FindRoutes();

			return routes.Any() ? routes.Select(r => r.A * 3 + r.B).Min() : 0;
		}

		private IEnumerable<(long A, long B)> FindRoutes(int max = 100)
		{
			for (var a = 0; a < max; a++)
			for (var b = 0; b < max; b++)
				if (ButtonA.X * a + ButtonB.X * b == Prize.X &&
					ButtonA.Y * a + ButtonB.Y * b == Prize.Y)
					yield return (a, b);
		}
	}
}