namespace AdventOfCode2018;

public static class Day21
{
    public static void Solve()
    {
        var instructions = File.ReadAllLines("AdventOfCode/2018/21/Input.txt").Skip(1).Select(row =>
            (OpCode: row.Split(' ')[0], Parameter: row.Split(' ').Skip(1).Select(int.Parse).ToArray())).ToArray();

        int value = 1_000_000_000;

        while (Run(instructions, value++) > 900_000)
            Console.WriteLine(value);

        Console.WriteLine($"Part 1: {value}, Part 2: {0}");

        int Run((string OpCode, int[] Parameter)[] program, int start)
        {
            const int ip = 5;
            int c = 0;

            var registers = new int[6];
            registers[0] = start;

            do
                Execute(program[registers[ip]].OpCode, program[registers[ip]].Parameter, registers);
            while (registers[ip]++ < program.Length && c++ < 1_000_000);

            return c;
        }

        void Execute(string opCode, int[] parameters, int[] registers)
        {
            registers[parameters[2]] = opCode switch
            {
                "bani" => registers[parameters[0]] & parameters[1],
                "gtri" => registers[parameters[0]] > parameters[1] ? 1 : 0,
                "seti" => parameters[0],
                "eqrr" => registers[parameters[0]] == registers[parameters[1]] ? 1 : 0,
                "eqir" => parameters[0] == registers[parameters[1]] ? 1 : 0,
                "borr" => registers[parameters[0]] | registers[parameters[1]],
                "bori" => registers[parameters[0]] | parameters[1],
                "banr" => registers[parameters[0]] & registers[parameters[1]],
                "muli" => registers[parameters[0]] * parameters[1],
                "eqri" => registers[parameters[0]] == parameters[1] ? 1 : 0,
                "mulr" => registers[parameters[0]] * registers[parameters[1]],
                "gtrr" => registers[parameters[0]] > registers[parameters[1]] ? 1 : 0,
                "setr" => registers[parameters[0]],
                "addr" => registers[parameters[0]] + registers[parameters[1]],
                "gtir" => parameters[0] > registers[parameters[1]] ? 1 : 0,
                "addi" => registers[parameters[0]] + parameters[1],
                _ => throw new ArgumentException(opCode)
            };

            //Console.WriteLine($"{opCode} {string.Join('-', parameters)}: \t{string.Join('\t', registers)}");
        }
    }
}