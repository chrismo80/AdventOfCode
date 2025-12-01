namespace AdventOfCode2016;

public static class Day1
{
	public static IEnumerable<object> Solve(string input)
	{
		var moves = input.Split(", ");

		var (X, Y) = (0, 0);
		var trail = new HashSet<(int, int)>();
		var view = 0;
		var result2 = (X: 0, Y: 0);

		foreach (var move in moves)
		{
			view = (view + 4 + (move[0] == 'L' ? -1 : 1)) % 4;

			for (var i = 0; i < int.Parse(new string(move.Skip(1).ToArray())); i++)
			{
				X = view == 1 ? X + 1 : view == 3 ? X - 1 : X;
				Y = view == 2 ? Y + 1 : view == 0 ? Y - 1 : Y;

				if (!trail.Add((X, Y)))
					result2 = result2 == (0, 0) ? (X, Y) : result2;
			}
		}

		yield return Math.Abs(X) + Math.Abs(Y);
		yield return Math.Abs(result2.X) + Math.Abs(result2.Y);
	}
}