namespace AdventOfCode2020;
public static class Day5
{
    public static void Solve()
    {
        var seatIds = File.ReadAllLines("AdventOfCode/2020/05/Input.txt")
            .Select(code => (8 * Position(code.Take(7), 'B', 128)) + Position(code.Skip(7), 'R', 8));

        Console.WriteLine($"Part 1: {seatIds.Max()}, Part 2: {FirstEmpty(seatIds.Order())}");

        static int Position(IEnumerable<char> code, char direction, int end) =>
            code.Aggregate((Min: 0, Max: end), (last, current) =>
                (
                    Min: current != direction ? last.Min : last.Min + ((last.Max - last.Min) / 2),
                    Max: current == direction ? last.Max : last.Min + ((last.Max - last.Min) / 2)
                )).Min;

        static int FirstEmpty(IEnumerable<int> seatIds) =>
            seatIds.Aggregate((Id: seatIds.First(), Empty: 0), (last, current) =>
                (
                    Id: current,
                    Empty: current - last.Id > 1 && last.Empty == 0 ? last.Id + 1 : last.Empty
                )).Empty;
    }
}