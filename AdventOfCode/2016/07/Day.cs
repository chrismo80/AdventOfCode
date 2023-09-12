namespace AdventOfCode2016
{
    public static class Day7
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2016/07/Input.txt")
                .Select(row => row.Split('[', ']'));

            var result1 = input.Count(ip =>
                ip.Where((_, i) => i % 2 != 0).All(part => !GetAllABBA(part).Any()) &&
                ip.Where((_, i) => i % 2 == 0).Any(part => GetAllABBA(part).Any()));

            var result2 = input.Count(ip => ip
                .Where((_, i) => i % 2 != 0).Select(part => GetAllABA(part)).SelectMany(aba => aba)
                .Intersect(ip.Where((_, i) => i % 2 == 0)
                    .Select(part => GetAllABA(part).Select(aba => InverseABA(aba))).SelectMany(bab => bab)
                    ).Any());

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

            string[] GetAllABBA(string part) => Enumerable.Range(-1, part.Length - 3)
                .Where(i => part[i + 1] != part[i + 2] && part[i + 1] == part[i + 4] && part[i + 2] == part[i + 3])
                .Select(i => part.Substring(i + 1, 4)).ToArray();

            string[] GetAllABA(string part) => Enumerable.Range(-1, part.Length - 2)
                .Where(i => part[i + 1] != part[i + 2] && part[i + 1] == part[i + 3])
                .Select(i => part.Substring(i + 1, 3)).ToArray();

            string InverseABA(string part) => new(part.Append(part[1]).Skip(1).ToArray());
        }
    }
}