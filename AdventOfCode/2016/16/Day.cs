namespace AdventOfCode2016;
public static class Day16
{
    public static void Solve()
    {
        var input = File.ReadAllText("AdventOfCode/2016/16/Input.txt");

        Console.WriteLine($"Part 1: {GetCheckSum(input, 272)}, Part 2: {GetCheckSum(input, 35651584)}");

        static string GetCheckSum(string data, int diskLength)
        {
            while (data.Length < diskLength)
                data += 0 + string.Concat(data.Reverse().Select(digit => '1' - digit));
            data = data[..diskLength];

            while (data.Length % 2 == 0)
                data = string.Concat(data.Chunk(2).Select(pair => pair[0] == pair[1] ? 1 : 0));

            return data;
        }
    }
}