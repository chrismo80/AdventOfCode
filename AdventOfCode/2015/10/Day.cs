namespace AdventOfCode2015
{
    public static class Day10
    {
        public static void Solve()
        {
            var sequence = File.ReadAllLines("AdventOfCode/2015/10/Input.txt")[0];

            for (int i = 0; i < 50; i++)
            {
                var builder = new System.Text.StringBuilder();
                char number = sequence[0];
                int count = 0;

                foreach (char letter in sequence)
                {
                    if (letter != number)
                    {
                        builder.Append(count).Append(number);
                        number = letter;
                        count = 0;
                    }

                    count++;
                }

                sequence = builder.Append(count).Append(number).ToString();
            }

            Console.WriteLine(sequence.Length);
        }
    }
}