namespace AdventOfCode2015;
public static class Day23
{
    public static void Solve()
    {
        var program = File.ReadAllLines("AdventOfCode/2015/23/Input.txt");

        Console.WriteLine($"Part 1: {Run(0)}, Part 1: {Run(1)}");

        int Run(int start, int i = 0)
        {
            var registers = new Dictionary<char, int>() { { 'a', start }, { 'b', 0 } };

            while (i < program.Length)
            {
                var cmd = program[i].Split(' ', ',');

                switch (cmd[0])
                {
                    case "inc": registers[cmd[1][0]]++; i++; break;
                    case "hlf": registers[cmd[1][0]] /= 2; i++; break;
                    case "tpl": registers[cmd[1][0]] *= 3; i++; break;
                    case "jmp": i += int.Parse(cmd[1]); break;
                    case "jie": i += registers[cmd[1][0]] % 2 == 0 ? int.Parse(cmd[3]) : 1; break;
                    case "jio": i += registers[cmd[1][0]] == 1 ? int.Parse(cmd[3]) : 1; break;
                }
            }

            return registers['b'];
        }
    }
}