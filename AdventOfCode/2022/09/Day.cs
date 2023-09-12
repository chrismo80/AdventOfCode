namespace AdventOfCode2022;
public static class Day9
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/09/Input.txt")
            .Select(l => (l.Split(' ')[0], int.Parse(l.Split(' ')[1]))).ToArray();

        var result1 = Run(input, new (int, int)[2]);
        var result2 = Run(input, new (int, int)[10]);

        Console.WriteLine($"Part 1: {result1} (6067), Part 2: {result2} (2471)");

        int Run((string Direction, int Distance)[] moves, (int, int)[] rope) =>
            moves.Select(m => new int[m.Distance].Select(_ => MoveRope(rope, m.Direction)))
                .SelectMany(t => t).Distinct().Count();

        (int, int) MoveRope((int, int)[] rope, string direction)
        {
            rope[0] = MoveHead(rope[0], direction);

            for (int i = 1; i < rope.Length; i++)
                rope[i] = MoveTail(rope[i - 1], rope[i]);

            return rope.Last();
        }

        (int, int) MoveHead((int X, int Y) pos, string direction) => (
            pos.X + (direction == "U" ? -1 : direction == "D" ? 1 : 0),
            pos.Y + (direction == "L" ? -1 : direction == "R" ? 1 : 0));

        (int, int) MoveTail((int X, int Y) prev, (int X, int Y) next) => Done(prev, next) ? next : (
            next.X += Math.Sign(prev.X - next.X),
            next.Y += Math.Sign(prev.Y - next.Y));

        bool Done((int X, int Y) prev, (int X, int Y) next) =>
            Math.Abs(prev.X - next.X) < 2 &&
            Math.Abs(prev.Y - next.Y) < 2;
    }
}