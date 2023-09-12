namespace AdventOfCode2017
{
    public static class Day2
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2017/02/Input.txt")
                .Select(row => row.Split('\t').Select(int.Parse));

            var result1 = input.Sum(row => row.Max() - row.Min());
            var result2 = input.Select(row => FindNumbers(row)).Sum(n => n.X / n.Y);

            Console.WriteLine($"Part 1: {result1}");
            Console.WriteLine($"Part 2: {result2}");

            static (int X, int Y) FindNumbers(IEnumerable<int> row) =>
                (from x in row from y in row where x != y && x % y == 0 select (x, y)).Single();
        }
    }
}