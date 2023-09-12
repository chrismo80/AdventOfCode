namespace AdventOfCode2021
{
    public static class Day21
    {
        public static void Solve()
        {
            var players = File.ReadAllLines("AdventOfCode/2021/21/Input.txt")
                .Select(row => row.Last().ToString()).Select(int.Parse).ToArray();

            int dice = 1, rolled = 0, diceSides = 100, win = 1000;

            var scores = new List<int>() { 0, 0 };

            while (scores.All(s => s < win))
            {
                for (int i = 0; i < players.Length; i++)
                {
                    var move = Enumerable.Range(0, 3).Sum(d => ((dice + d - 1) % diceSides) + 1);
                    dice = ((dice + 3 - 1) % diceSides) + 1;
                    rolled += 3;

                    players[i] = ((players[i] + move - 1) % 10) + 1;
                    scores[i] += players[i];

                    if (scores.Any(s => s >= win)) break;
                }
            }

            Console.WriteLine($"{scores.Min()} * {rolled} = {scores.Min() * rolled}");
        }
    }
}