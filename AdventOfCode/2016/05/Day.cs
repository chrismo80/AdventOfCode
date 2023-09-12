namespace AdventOfCode2016;
public static class Day5
{
    public static void Solve()
    {
        const string input = "wtnhxymk";

        var result1 = new List<char>();
        var result2 = new List<char>();
        var result2Pos = new List<char>();

        for (int i = 0; result1.Count < 8; i++)
        {
            var hash = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(input + i.ToString()));
            var hex = BitConverter.ToString(hash);

            if (hex.StartsWith("00-00-0"))
                result1.Add(hex[7]);
        }

        for (int i = 0; result2.Count < 8; i++)
        {
            var hash = System.Security.Cryptography.MD5.HashData(System.Text.Encoding.UTF8.GetBytes(input + i.ToString()));
            var hex = BitConverter.ToString(hash);

            if (hex.StartsWith("00-00-0") && char.IsDigit(hex[7]) && int.Parse(hex[7].ToString()) < 8 && !result2Pos.Contains(hex[7]))
            {
                result2Pos.Add(hex[7]);
                result2.Add(hex[9]);
            }
        }

        var pass = result2.Zip(result2Pos, (v, i) => (Pos: int.Parse(i.ToString()), Char: v)).OrderBy(x => x.Pos).Select(x => x.Char);

        Console.WriteLine($"Part 1: {new string(result1.ToArray())}, Part 2: {new string(pass.ToArray())}");
    }
}