namespace AdventOfCode2018;

public static class Day19
{
    public static void Solve()
    {
        var instructions = File.ReadAllLines("AdventOfCode/2018/19/Input.txt").Skip(1).Select(row =>
            (OpCode: row.Split(' ')[0], Parameter: row.Split(' ').Skip(1).Select(int.Parse).ToArray())).ToArray();

        const int ip = 3;
        var registers = new int[6];
        registers[0] = 1;

        do
            Execute(instructions[registers[ip]].OpCode, instructions[registers[ip]].Parameter);
        while (registers[ip]++ < instructions.Length);

        Console.WriteLine($"Part 1: {registers[0]}, Part 2: {0}");

        void Execute(string opCode, int[] parameters)
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
        }
    }
}