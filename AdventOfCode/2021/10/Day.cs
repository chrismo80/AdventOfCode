namespace AdventOfCode2021
{
    public static class Day10
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/10/Input.txt")
                .Select(TrimLines).Where(l => l != "");

            var result1 = input.Sum(CorruptPoints);

            var points = input.Select(IncompletePoints).Where(p => p > 0).Order();
            var result2 = points.Chunk((points.Count() / 2) + 1).First().Last();

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }

        private static string TrimLines(string line)
        {
            int length = 0;
            while (line.Length > 0 && line.Length != length)
            {
                length = line.Length;
                line = line.Replace("()", "").Replace("[]", "").Replace("{}", "").Replace("<>", "");
            }
            return line;
        }

        private static long CorruptPoints(string line)
        {
            char c = line.Intersect(")]}>".ToArray()).FirstOrDefault();
            return c == ')' ? 3 : c == ']' ? 57 : c == '}' ? 1197 : c == '>' ? 25137 : 0;
        }

        private static long IncompletePoints(string line)
        {
            if (line.Intersect(")]}>".ToArray()).Any())
                return 0;

            long points = 0;
            foreach (char c in line.Reverse())
            {
                points *= 5;
                points += c == '(' ? 1 : c == '[' ? 2 : c == '{' ? 3 : c == '<' ? 4 : 0;
            }
            return points;
        }
    }
}