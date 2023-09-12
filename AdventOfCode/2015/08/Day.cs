namespace AdventOfCode2015
{
    using System.Text.RegularExpressions;
    public static class Day8
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/08/Input.txt");

            var sizeInCode = input.Select(text => text.Length);

            var memory = input
                .Select(text => Regex.Unescape(text))
                .Select(text => text[1..^1].Replace(@"\", "-").Replace("\"", "-"));

            var sizeInMemory = memory.Select(text => text.Length);

            var encoded = input
                .Select(text => text.Replace("\\", @"\\").Replace("\"", @"\"""))
                .Select(text => "\"" + text + "\"");

            var sizeEncoded = encoded.Select(text => text.Length);

            Console.WriteLine($"Part 1: {sizeInCode.Sum() - sizeInMemory.Sum()}");
            Console.WriteLine($"Part 2: {sizeEncoded.Sum() - sizeInCode.Sum()}");
        }
    }
}