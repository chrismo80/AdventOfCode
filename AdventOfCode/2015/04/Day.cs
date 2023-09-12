namespace AdventOfCode2015
{
    using System.Text;
    using System.Security.Cryptography;

    public static class Day4
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/04/Input.txt")[0];

            var result1 = Enumerable.Range(0, 100000000)
                .First(i => GetMD5(input + i).StartsWith("00000"));

            var result2 = Enumerable.Range(0, 100000000)
                .First(i => GetMD5(input + i).StartsWith("000000"));

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

            static string GetMD5(string text) =>
                Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(text)));
        }
    }
}