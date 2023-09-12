namespace AdventOfCode2019;
public static class Day1
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2019/01/Input.txt").Select(int.Parse);

        var result1 = input.Sum(mass => (mass / 3) - 2);
        var result2 = input.Sum(mass => GetTotalFuel(mass));

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        static int GetTotalFuel(int mass)
        {
            var fuel = (mass / 3) - 2;
            var total = fuel;

            while (fuel > 0)
            {
                fuel = (fuel / 3) - 2;
                total += Math.Max(fuel, 0);
            }

            return total;
        }
    }
}