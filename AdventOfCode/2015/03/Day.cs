namespace AdventOfCode2015
{
    public static class Day3
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/03/Input.txt")[0];

            var houses1 = new List<(int X, int Y)> { (0, 0) };
            var houses2 = new List<(int X, int Y)> { (0, 0) };

            static (int, int) NewHouse((int X, int Y) lastHouse, char direction) => (
                lastHouse.X + (direction == 'v' ? 1 : direction == '^' ? -1 : 0),
                lastHouse.Y + (direction == '>' ? 1 : direction == '<' ? -1 : 0));

            foreach (char move in input)
                houses1.Add(NewHouse(houses1.Last(), move));

            foreach (char move in input.Where((_, i) => i % 2 == 0))
                houses2.Add(NewHouse(houses2.Last(), move));

            houses2.Add((0, 0));

            foreach (char move in input.Where((_, i) => i % 2 != 0))
                houses2.Add(NewHouse(houses2.Last(), move));

            var result1 = houses1.Distinct().Count();
            var result2 = houses2.Distinct().Count();

            Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
        }
    }
}