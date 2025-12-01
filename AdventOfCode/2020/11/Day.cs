using AdventOfCode;

namespace AdventOfCode2020;

public static class Day11
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines();

		var seats = (from y in Enumerable.Range(0, lines.Length)
				from x in Enumerable.Range(0, lines[0].Length)
				where lines[y][x] == 'L'
				select (X: x, Y: y))
			.ToHashSet();

		yield return 0; //FinalSeatsOccupied(seats, 1);
		yield return FinalSeatsOccupied(seats, 2);

		static int FinalSeatsOccupied(HashSet<(int X, int Y)> seats, int part)
		{
			var occupied = new HashSet<(int X, int Y)>();
			var toBeSeated = new HashSet<(int X, int Y)>();
			var toBeLeft = new HashSet<(int X, int Y)>();

			do
			{
				PrintMap(seats, occupied);

				toBeSeated.Clear();
				toBeLeft.Clear();

				foreach (var seat in seats.Except(occupied))
				{
					if (part == 1 && !Neighbours1(seat, occupied).Any())
						toBeSeated.Add(seat);

					if (part == 2 && !Neighbours2(seat, occupied).Any())
						toBeSeated.Add(seat);
				}

				foreach (var seat in occupied)
				{
					if (part == 1 && Neighbours1(seat, occupied).Count >= 4)
						toBeLeft.Add(seat);

					var n = Neighbours2(seat, occupied);

					if (part == 2 && Neighbours2(seat, occupied).Count >= 5)
						toBeLeft.Add(seat);
				}

				occupied.UnionWith(toBeSeated);
				occupied.RemoveWhere(seat => toBeLeft.Contains(seat));
			} while (toBeSeated.Any() || toBeLeft.Any());

			return occupied.Count;
		}

		static HashSet<(int X, int Y)> Neighbours1((int X, int Y) seat, HashSet<(int X, int Y)> occupied)
		{
			return occupied.Where(n => Math.Abs(n.X - seat.X) < 2 && Math.Abs(n.Y - seat.Y) < 2 && n != seat).ToHashSet();
		}

		static HashSet<(int X, int Y)> Neighbours2((int X, int Y) seat, HashSet<(int X, int Y)> occupied)
		{
			return occupied.Where(n =>
				( // First of all 8 directions only, missing implementation
					Math.Abs(n.X - seat.X) == Math.Abs(n.Y - seat.Y) ||
					n.X == seat.X ||
					n.Y == seat.Y
				) && n != seat).ToHashSet();
		}
	}

	private static void PrintMap(HashSet<(int X, int Y)> seats, HashSet<(int X, int Y)> occupied)
	{
		Thread.Sleep(100);
		var output = new List<string>();

		for (var y = seats.Min(w => w.Y); y <= seats.Max(w => w.Y); y++)
		{
			var row = new List<char>();

			for (var x = seats.Min(w => w.X); x <= seats.Max(w => w.X); x++)
				row.Add(occupied.Contains((x, y)) ? '#' : seats.Contains((x, y)) ? 'L' : '.');

			output.Add(new string(row.ToArray()));
		}

		Output.Save(2020, 11, string.Join(Environment.NewLine, output));
	}
}