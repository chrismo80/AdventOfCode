namespace AdventOfCode2020;
public static class Day22
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/22/Input.txt");

        var player1 = new Queue<int>();
        var player2 = new Queue<int>();

        input.Chunk((input.Length / 2) + 1).First().Skip(1).SkipLast(1).Select(int.Parse).ToList()
            .ForEach(card => player1.Enqueue(card));

        input.Chunk((input.Length / 2) + 1).Last().Skip(1).Select(int.Parse).ToList()
            .ForEach(card => player2.Enqueue(card));

        while (player1.TryDequeue(out int card1) && player2.TryDequeue(out int card2))
        {
            if (card1 > card2)
            { player1.Enqueue(card1); player1.Enqueue(card2); }
            else
            { player2.Enqueue(card2); player2.Enqueue(card1); }
        }

        var result1 = player1.Concat(player2).Reverse().Select((card, i) => card * (i + 1)).Sum();

        Console.WriteLine($"Part 1: {result1}, Part 2: {0}");
    }
}