namespace AdventOfCode2019;
public static class Day2
{
    public static void Solve()
    {
        var program = File.ReadAllLines("AdventOfCode/2019/02/Input.txt")[0]
            .Split(',').Select(int.Parse).ToArray();

        var result1 = RunProgram(program, 12, 2);

        var result2 = (from Noun in Enumerable.Range(0, 100)
                       from Verb in Enumerable.Range(0, 100)
                       where RunProgram(program, Noun, Verb) == 19690720
                       select (Noun, Verb)).Single();

        Console.WriteLine($"Part 1: {result1}, Part 2: {(result2.Noun * 100) + result2.Verb}");

        static int RunProgram(int[] memory, int noun, int verb)
        {
            var program = memory.Select(address => address).ToArray();

            program[1] = noun;
            program[2] = verb;

            for (int i = 0; i < program.Length; i += 4)
            {
                if (program[i] == 99)
                    return program[0];

                if (program[i] == 1)
                    program[program[i + 3]] = program[program[i + 1]] + program[program[i + 2]];

                if (program[i] == 2)
                    program[program[i + 3]] = program[program[i + 1]] * program[program[i + 2]];
            }
            return -1;
        }
    }
}