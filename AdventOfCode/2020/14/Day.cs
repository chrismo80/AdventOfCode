namespace AdventOfCode2020;
public static class Day14
{
    public static void Solve()
    {
        var program = File.ReadAllLines("AdventOfCode/2020/14/Input.txt");

        Console.WriteLine($"Part 1: {program.Run(1).Values.Sum()}");
        Console.WriteLine($"Part 2: {program.Run(2).Values.Sum()}");
    }

    static Dictionary<long, long> Run(this string[] program, int version)
    {
        long address, value;
        var memory = new Dictionary<long, long>();
        string mask = "";

        foreach (var line in program)
        {
            if (line.StartsWith("mask"))
            {
                mask = line.Split(" = ")[1];
                continue;
            }

            address = long.Parse(line.Split('[', ']')[1]);
            value = long.Parse(line.Split(" = ")[1]);

            switch (version)
            {
                case 1:
                    memory[address] = value.ApplyV1(mask);
                    break;
                case 2:
                    foreach (var add in address.ApplyV2(mask))
                        memory[add] = value;
                    break;
            }
        }
        return memory;
    }

    static long ApplyV1(this long value, string mask)
    {
        string masked = "", unmasked = Convert.ToString(value, 2).PadLeft(mask.Length, '0');

        for (int i = 1; i <= mask.Length; i++)
            masked = (mask[^i] == 'X' ? unmasked[^i] : mask[^i]) + masked;

        return Convert.ToInt64(masked, 2);
    }

    static long[] ApplyV2(this long value, string mask)
    {
        var values = new List<long>();
        var permutations = new List<string>();
        string masked = "", unmasked = Convert.ToString(value, 2).PadLeft(mask.Length, '0');

        for (int i = 1; i <= mask.Length; i++)
            masked = (mask[^i] == '0' ? unmasked[^i] : mask[^i]) + masked;

        var positions = masked.Select((c, i) => (c, i)).Where(x => x.c == 'X').Select(x => x.i).ToArray();

        for (int i = 0; i < Math.Pow(2, positions.Length); i++)
            permutations.Add(Convert.ToString(i, 2).PadLeft(positions.Length, '0'));

        foreach (var floating in permutations)
        {
            for (int i = 0; i < floating.Length; i++)
                masked = masked.Remove(positions[i], 1).Insert(positions[i], $"{floating[i]}");

            values.Add(Convert.ToInt64(masked, 2));
        }

        return values.ToArray();
    }
}