namespace AdventOfCode2020;
public static class Day8
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/08/Input.txt").Select(l => l.Split(' ')).ToArray();

        RunProgram(input, out int acc1);

        var inverted = new List<int>();
        var program = input;
        int acc2;

        while (!RunProgram(program, out acc2))
        {
            for (int i = 0; i < program.Length; i++)
            {
                if (program[i][0] != "acc" && !inverted.Contains(i))
                {
                    program = input.Select(cmd => cmd.Select(x => x).ToArray()).ToArray();
                    program[i][0] = program[i][0] == "jmp" ? "nop" : "jmp";
                    inverted.Add(i);
                    break;
                }
            }
        }

        Console.WriteLine($"Part 1: {acc1}, Part 2: {acc2}");

        static bool RunProgram(string[][] program, out int acc)
        {
            acc = 0;
            int pointer = 0;
            var executed = new List<int>();

            while (pointer < program.Length)
            {
                executed.Add(pointer);
                acc += program[pointer][0] == "acc" ? int.Parse(program[pointer][1]) : 0;
                pointer += program[pointer][0] == "jmp" ? int.Parse(program[pointer][1]) : 1;

                if (executed.Contains(pointer))
                    return false;
            }
            return true;
        }
    }
}