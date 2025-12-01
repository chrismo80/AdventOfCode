using AdventOfCode;

namespace AdventOfCode2020;

public static class Day12
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.Lines()
			.Select(row => (action: row[0], value: int.Parse(row[1..]))).ToArray();

		var ship = (X: 0, Y: 0, F: 1); // 0: north, 1: east, 2: south, 3: west
		var wayPoint = (X: 10, Y: -1);

		foreach (var command in lines)
			MoveShip(command, ref ship);

		yield return Math.Abs(ship.X) + Math.Abs(ship.Y);

		ship = (X: 0, Y: 0, F: 1);

		foreach (var command in lines)
			MoveWayPoint(command, ref ship, ref wayPoint);

		yield return Math.Abs(ship.X) + Math.Abs(ship.Y);

		void MoveShip((char Action, int Value) cmd, ref (int X, int Y, int Face) pos)
		{
			switch (cmd.Action)
			{
				case 'N': pos.Y -= cmd.Value; break;
				case 'S': pos.Y += cmd.Value; break;
				case 'E': pos.X += cmd.Value; break;
				case 'W': pos.X -= cmd.Value; break;
				case 'L':
					pos.Face -= cmd.Value / 90;
					pos.Face += 4;
					pos.Face %= 4;
					break;
				case 'R':
					pos.Face += cmd.Value / 90;
					pos.Face %= 4;
					break;
				case 'F':
					pos.X += pos.Face == 1 ? cmd.Value : pos.Face == 3 ? cmd.Value * -1 : 0;
					pos.Y += pos.Face == 2 ? cmd.Value : pos.Face == 0 ? cmd.Value * -1 : 0;
					break;
			}
		}

		void MoveWayPoint((char Action, int Value) cmd,
			ref (int X, int Y, int Face) ship, ref (int X, int Y) wayPoint)
		{
			var (dX, dY) = (wayPoint.X - ship.X, wayPoint.Y - ship.Y);

			switch (cmd.Action)
			{
				case 'N': wayPoint.Y -= cmd.Value; break;
				case 'S': wayPoint.Y += cmd.Value; break;
				case 'E': wayPoint.X += cmd.Value; break;
				case 'W': wayPoint.X -= cmd.Value; break;
				case 'L' or 'R':
					for (var i = 90; i <= cmd.Value; i += 90)
					{
						wayPoint.X = ship.X + (cmd.Action == 'R' ? -dY : dY);
						wayPoint.Y = ship.Y + (cmd.Action == 'L' ? -dX : dX);
						(dX, dY) = (wayPoint.X - ship.X, wayPoint.Y - ship.Y);
					}

					break;
				case 'F':
					ship.X += dX * cmd.Value;
					wayPoint.X = ship.X + dX;
					ship.Y += dY * cmd.Value;
					wayPoint.Y = ship.Y + dY;
					break;
			}
		}
	}
}