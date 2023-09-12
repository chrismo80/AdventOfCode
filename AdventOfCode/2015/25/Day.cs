namespace AdventOfCode2015;
public static class Day25
{
    public static void Solve()
    {
        long code = 20151125, row = 3010, col = 3019, diagonal = 1, count = 1;

        while (row + col > 1 + diagonal++)
            count += diagonal;

        while (count-- > row)
            code = code * 252533 % 33554393;

        Console.WriteLine($"Part 1: {code}");
    }
}