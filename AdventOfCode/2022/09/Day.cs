using AdventOfCode;

namespace AdventOfCode2022;

public static class Day9
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(l => (l.Split(' ')[0], int.Parse(l.Split(' ')[1]))).ToArray();

		yield return Run(data, new (int, int)[2]);
		yield return Run(data, new (int, int)[10]);

		int Run((string Direction, int Distance)[] moves, (int, int)[] rope)
		{
			return moves.Select(m => new int[m.Distance].Select(_ => MoveRope(rope, m.Direction)))
				.SelectMany(t => t).Distinct().Count();
		}

		(int, int) MoveRope((int, int)[] rope, string direction)
		{
			rope[0] = MoveHead(rope[0], direction);

			for (var i = 1; i < rope.Length; i++)
				rope[i] = MoveTail(rope[i - 1], rope[i]);

			return rope.Last();
		}

		(int, int) MoveHead((int X, int Y) pos, string direction)
		{
			return (
				pos.X + (direction == "U" ? -1 : direction == "D" ? 1 : 0),
				pos.Y + (direction == "L" ? -1 : direction == "R" ? 1 : 0));
		}

		(int, int) MoveTail((int X, int Y) prev, (int X, int Y) next)
		{
			return Done(prev, next) ? next : (
				next.X += Math.Sign(prev.X - next.X),
				next.Y += Math.Sign(prev.Y - next.Y));
		}

		bool Done((int X, int Y) prev, (int X, int Y) next)
		{
			return Math.Abs(prev.X - next.X) < 2 &&
				Math.Abs(prev.Y - next.Y) < 2;
		}
	}
}