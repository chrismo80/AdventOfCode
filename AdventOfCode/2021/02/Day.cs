namespace AdventOfCode2021
{
    public static class Day2
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/02/Input.txt")
                .Select(l => (Dir: l[0], Value: int.Parse(l.Split(" ")[1])));

            var result1 = input.Aggregate((Hor: 0, Depth: 0), (last, current) =>
                (
                    Hor: current.Dir == 'f' ? last.Hor + current.Value : last.Hor,
                    Depth: current.Dir == 'd' ? last.Depth + current.Value :
                            current.Dir == 'u' ? last.Depth - current.Value : last.Depth
                ),
                    result => result.Hor * result.Depth
                );

            var result2 = input.Aggregate((Hor: 0, Depth: 0, Aim: 0), (last, current) =>
                (
                    Hor: current.Dir == 'f' ? last.Hor + current.Value : last.Hor,
                    Depth: current.Dir == 'f' ? last.Depth + (last.Aim * current.Value) : last.Depth,
                    Aim: current.Dir == 'd' ? last.Aim + current.Value :
                            current.Dir == 'u' ? last.Aim - current.Value : last.Aim
                ),
                    result => result.Hor * result.Depth
                );

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}