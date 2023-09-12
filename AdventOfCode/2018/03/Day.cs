namespace AdventOfCode2018;
public static class Day3
{
    public static void Solve()
    {
        var claims = File.ReadAllLines("AdventOfCode/2018/03/Input.txt")
            .Select(l => l.Split('#', '@', ',', ':', 'x').Skip(1).Select(int.Parse).ToArray())
            .Select(d => (Id: d[0], X: d[1], Y: d[2], W: d[3], H: d[4], Positions: new HashSet<int>()))
            .ToArray();

        var squareInches = new int[1_0000_0000];

        foreach (var claim in claims)
        {
            foreach (var pos in from X in Enumerable.Range(claim.X, claim.W)
                                from Y in Enumerable.Range(claim.Y, claim.H)
                                select (X * 1_0000) + Y)
            {
                squareInches[pos]++;
                claim.Positions.Add(pos);
            }
        }

        var result1 = squareInches.Count(count => count > 1);
        var result2 = claims.Single(claim => claim.Positions.All(pos => squareInches[pos] == 1)).Id;

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");
    }
}