namespace AdventOfCode2021
{
    public static class Day6
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2021/06/Input.txt")
                [0].Split(",").Select(int.Parse).ToArray();

            Console.WriteLine($"Part 1: {input.PopulationAfter(80)}, Part 2: {input.PopulationAfter(256)}");
        }

        private static long PopulationAfter(this int[] parents, int days)
        {
            IEnumerable<int> Birthdays(int age, int days) => Enumerable.Range(0, days / 6)
                .Select(x => (x * 7) + age).Where(x => x < days);

            // setup array for each day and count all childrens born on that day
            long[] childrens = new long[days];

            foreach (var age in parents)
            {   // setup children from initial parents
                foreach (var birthday in Birthdays(age, days))
                    childrens[birthday]++;
            }

            for (int day = 0; day < days; day++)
            {   // add new childrens each day from children on that day
                foreach (var birthday in Birthdays(8, days - day - 1))
                    childrens[day + 1 + birthday] += childrens[day];
            }

            return childrens.Sum() + parents.Length;
        }
    }
}