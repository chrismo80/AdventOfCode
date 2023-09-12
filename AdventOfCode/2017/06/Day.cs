namespace AdventOfCode2017
{
    public static class Day6
    {
        public static void Solve()
        {
            var memory = File.ReadAllText("AdventOfCode/2017/06/Input.txt")
                .Split("\t").Select(int.Parse).ToList();

            var history = new HashSet<string>();
            int pos, value;

            while (history.Add(string.Join(',', memory)))
            {
                // find max stack
                value = memory.Max();
                pos = memory.IndexOf(value);

                // clear max stack
                memory[pos] = 0;

                // spread values from max through memory neighbors
                while (value-- > 0)
                    memory[++pos % memory.Count]++;
            }

            var result2 = history.Count - history.ToList().IndexOf(string.Join(',', memory));

            Console.WriteLine($"Part 1: {history.Count}, Part 2: {result2}");
        }
    }
}