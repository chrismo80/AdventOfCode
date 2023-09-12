namespace AdventOfCode2016
{
    public static class Day4
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2016/04/Input.txt")
                .Select(row => row.Split('-', '[', ']').SkipLast(1))
                .Select(items => (
                    Name: items.SkipLast(2),
                    SectorId: int.Parse(items.SkipLast(1).Last()),
                    Checksum: items.Last())).ToArray();

            var result1 = input.Where(room => Check(room.Name) == room.Checksum).Sum(room => room.SectorId);
            var result2 = input.First(room => Shift(room.Name, room.SectorId) == "northpole object storage");

            static string Check(IEnumerable<string> name) => string.Join("", name
                .Select(part => part.ToArray()).SelectMany(c => c)
                .GroupBy(c => c).OrderByDescending(g => g.Count()).ThenBy(g => g.Key)
                .SelectMany(c => c).Distinct().Take(5));

            static string Shift(IEnumerable<string> name, int sectorId) =>
                string.Join(' ', name.Select(word =>
                    new string(word.Select(c => (char)(((c - 'a' + sectorId) % 26) + 'a')).ToArray())));

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2.SectorId}");
        }
    }
}