namespace AdventOfCode2018
{
    public static class Day9
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2018/09/Input.txt")[0].Split(' ');

            int player = 0, marble = 0, lastMarble = int.Parse(input[6]) * 100;
            var scores = new long[int.Parse(input[0])];
            var marbles = new LinkedList<int>();
            var current = marbles.AddFirst(0);

            while (marble++ < lastMarble)
            {
                if (marble % 23 == 0)
                {
                    // move 6 marbles to the left
                    for (int i = 0; i < 6; i++)
                        current = current!.Previous ?? marbles.Last;

                    // update player score and remove left neighbor
                    scores[player] += marble + current!.Previous!.Value;
                    marbles.Remove(current.Previous);
                }
                else
                {
                    // add new marble between the two right neighbors
                    current = current!.Next ?? marbles.First;
                    marbles.AddAfter(current!, marble);
                    current = current!.Next ?? marbles.First;
                }

                player++;
                player %= scores.Length;
            }

            Console.WriteLine($"Max score: {scores.Max()}");
        }
    }
}