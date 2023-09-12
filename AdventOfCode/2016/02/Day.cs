namespace AdventOfCode2016
{
    public static class Day2
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2016/02/Input.txt");

            int key = 5;
            var code1 = new List<int>();
            var code2 = new List<string>();

            foreach (var line in input)
            {
                foreach (char c in line)
                {
                    key =
                        c == 'D' && key < 7 ? key + 3 : c == 'L' && (key - 1) % 3 != 0 ? key - 1 :
                        c == 'U' && key > 3 ? key - 3 : c == 'R' && key % 3 != 0 ? key + 1 : key;
                }
                code1.Add(key);
            }

            key = 5;
            foreach (var line in input)
            {
                foreach (char c in line)
                {
                    key =
                        c == 'U' && !new int[] { 5, 2, 1, 4, 9 }.Contains(key) ? key - (new int[] { 3, 13 }.Contains(key) ? 2 : 4) :
                        c == 'D' && !new int[] { 5, 10, 13, 12, 9 }.Contains(key) ? key + (new int[] { 1, 11 }.Contains(key) ? 2 : 4) :
                        c == 'L' && !new int[] { 1, 2, 5, 10, 13 }.Contains(key) ? key - 1 :
                        c == 'R' && !new int[] { 1, 4, 9, 12, 13 }.Contains(key) ? key + 1 : key;
                }
                code2.Add(key.ToString("X"));
            }

            Console.WriteLine($"Part 1: {string.Join("", code1)}");
            Console.WriteLine($"Part 2: {string.Join("", code2)}");
        }
    }
}