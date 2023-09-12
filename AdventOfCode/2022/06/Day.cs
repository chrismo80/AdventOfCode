namespace AdventOfCode2022;
public static class Day6
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/06/Input.txt")[0];

        int FindMarker1(string stream, int size) => Enumerable.Range(size, stream.Length)
            .First(i => stream.Skip(i - size).Take(size).Distinct().Count() == size);

        int FindMarker2(string stream, int size) => stream
            .TakeWhile((_, i) => stream.Skip(i).Take(size).Distinct().Count() < size)
            .Count() + size;

        Console.WriteLine($"Part 1: {FindMarker1(input, 4)}, Part 2: {FindMarker2(input, 14)}");
    }
}