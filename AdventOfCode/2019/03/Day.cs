using AdventOfCode;

namespace AdventOfCode2019;

public static class Day3
{
	public static IEnumerable<object> Solve(string input)
	{
		var wires = input.Lines()
			.Select(row => CreateWire(row.Split(',')));

		var intersections = wires.First().Intersect(wires.Last());

		yield return intersections.Select(cross => Math.Abs(cross.X) + Math.Abs(cross.Y))
			.Order().First();

		yield return intersections.Select(cross => wires.Select(wire => wire.TakeWhile(segment => segment != cross).Count() + 1))
			.Min(distances => distances.Sum());

		static HashSet<(int X, int Y)> CreateWire(string[] path)
		{
			var wire = new HashSet<(int X, int Y)>();
			var (X, Y) = (0, 0);

			foreach (var dir in path)
			{
				var length = int.Parse(new string(dir.Skip(1).ToArray()));

				switch (dir[0])
				{
					case 'R':
						foreach (var segment in Enumerable.Range(X + 1, length))
							wire.Add((segment, Y));
						X += length;
						break;

					case 'L':
						foreach (var segment in Enumerable.Range(X - length, length).Reverse())
							wire.Add((segment, Y));
						X -= length;
						break;

					case 'D':
						foreach (var segment in Enumerable.Range(Y + 1, length))
							wire.Add((X, segment));
						Y += length;
						break;

					case 'U':
						foreach (var segment in Enumerable.Range(Y - length, length).Reverse())
							wire.Add((X, segment));
						Y -= length;
						break;
				}
			}

			return wire;
		}
	}
}