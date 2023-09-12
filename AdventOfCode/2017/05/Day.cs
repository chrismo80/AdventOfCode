namespace AdventOfCode2017
{
    public static class Day5
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2017/05/Input.txt").Select(int.Parse).ToArray();

            int pointer = 0, steps = 0, change;

            while (pointer < input.Length)
            {
                change = input[pointer] >= 3 ? -1 : 1; // 1;
                input[pointer] += change;
                pointer += input[pointer] - change;
                steps++;
            }

            Console.WriteLine(steps);
        }
    }
}