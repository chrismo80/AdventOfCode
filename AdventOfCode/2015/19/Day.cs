namespace AdventOfCode2015;
using Extensions;
public static class Day19
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2015/19/Input.txt");

        var replacements = input.SkipLast(2).Select(r => r.Split(" => ")).Select(r => (From: r[0], To: r[1]));
        var molecule = input.Last();

        var molecules = new HashSet<string>();

        foreach (var (From, To) in replacements)
        {
            foreach (var pos in molecule.AllIndexesOf(From))
                molecules.Add(new string(molecule.Take(pos).ToArray()) + To + new string(molecule.Skip(pos + From.Length).ToArray()));
        }

        int steps = 1;
        string before = "";

        while (molecule != "e" && molecule != before)
        {
            before = molecule;

            foreach (var (From, To) in replacements.Reverse().OrderBy(x => x.To.Length - x.From.Length))
            {
                foreach (var pos in molecule.AllIndexesOf(To))
                {
                    if (To.Length <= From.Length)
                        continue;

                    steps++;
                    molecule = new string(molecule.Take(pos).ToArray()) + From + new string(molecule.Skip(pos + To.Length).ToArray());
                    Console.WriteLine(molecule);
                }
            }
        }

        Console.WriteLine($"Part 1: {molecules.Count}, Part 2: {steps}");
    }
}