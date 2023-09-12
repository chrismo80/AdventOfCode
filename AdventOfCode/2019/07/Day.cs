namespace AdventOfCode2019;
using Extensions;
public static class Day7
{
    public static void Solve()
    {
        var program = File.ReadAllLines("AdventOfCode/2019/07/Input.txt")[0]
            .Split(',').Select(long.Parse).ToArray();

        var result1 = "01234".Permutations().Select(setting => AmplifiersChain(program, setting, false));
        var result2 = "56789".Permutations().Select(setting => AmplifiersChain(program, setting, true));

        Console.WriteLine($"Part 1: {result1.Max()}, Part 2: {result2.Max()}");

        static long AmplifiersChain(long[] program, IEnumerable<char> phaseSetting, bool loop = false)
        {
            //var amps = phaseSetting.Select(phase => new Amplifier(program, phase)).ToArray();
            var amps = phaseSetting.Select(phase => IntCodeComputer.Run(program, int.Parse(phase.ToString()), false)).ToArray();

            amps[0].Inputs.Enqueue(0);

            while (amps.Any(amp => amp.Running))
            {
                for (int i = 1; i < amps.Length; i++)
                {
                    if (amps[i].Running && amps[i - 1].Outputs.Count > 0)
                        amps[i].Inputs.Enqueue(amps[i - 1].Outputs.Dequeue());
                }

                if (loop && amps[0].Running && amps.Last().Outputs.Count > 0)
                    amps[0].Inputs.Enqueue(amps.Last().Outputs.Dequeue());
            }

            return amps.Last().Outputs.Last();
        }
    }


    public class IntCodeComputer
    {
        readonly Task _program;
        public bool Running { get; private set; } = true;
        public Dictionary<long, long> Memory { get; }
        public Queue<long> Inputs { get; set; } = new Queue<long>();
        public Queue<long> Outputs { get; set; } = new Queue<long>();
        public long Pointer { get; private set; }
        public long Phase { get; }
        public long RelativeBase { get; private set; }

        public IntCodeComputer(long[] memory, long input, bool wait)
        {
            Memory = Enumerable.Range(0, memory.Length).ToDictionary(i => (long)i, i => memory[i]);
            Inputs.Enqueue(input);
            Phase = input;
            _program = Task.Run(Start); //if (wait) _program.Wait();
        }

        public static IntCodeComputer Run(long[] memory, long input, bool wait = false) => new(memory, input, wait);

        private void Start()
        {
            while (Memory[Pointer] != 99)
            {
                switch (Memory[Pointer] % 100)
                {
                    case 1: Write(3, Read(1) + Read(2)); Pointer += 4; break;
                    case 2: Write(3, Read(1) * Read(2)); Pointer += 4; break;
                    case 3:
                        if (Inputs.Count > 0)
                        { Write(1, Inputs.Dequeue()); Pointer += 2; }
                        else
                        {
                            Thread.Sleep(0);
                        }
                        break;
                    case 4: Outputs.Enqueue(Read(1)); Pointer += 2; break;
                    case 5: Pointer = Read(1) != 0 ? Read(2) : Pointer + 3; break;
                    case 6: Pointer = Read(1) == 0 ? Read(2) : Pointer + 3; break;
                    case 7: Write(3, Read(1) < Read(2) ? 1 : 0); Pointer += 4; break;
                    case 8: Write(3, Read(1) == Read(2) ? 1 : 0); Pointer += 4; break;
                    case 9: RelativeBase += Read(1); Pointer += 2; break;
                }
            }
            Running = false;
        }

        public long GetOutput()
        {
            while (!Outputs.Any()) { Console.WriteLine("waiting for output"); }
            return Outputs.Dequeue();
        }

        private long Read(int offset) => Memory.TryGetValue(Parameter(offset), out long value) ? value : 0;

        private void Write(int offset, long value) { Memory[Parameter(offset)] = value; }

        private long Parameter(int offset) =>
            Memory[Pointer].ToString().PadLeft(5, '0')[3 - offset] switch
            {
                '0' => Memory.TryGetValue(Pointer + offset, out long value) ? value : 0,
                '1' => Pointer + offset,
                '2' => (Memory.TryGetValue(Pointer + offset, out long value) ? value : 0) + RelativeBase,
                _ => 0,
            };
    }


    public class Amplifier
    {
        readonly long[] _program;
        public bool Running => !Halted;
        public bool Halted { get; private set; }
        public Queue<long> Inputs { get; set; } = new Queue<long>();
        public Queue<long> Outputs { get; set; } = new Queue<long>();
        public int Phase { get; }
        public Amplifier(long[] memory, char phase)
        {
            _program = memory.Select(address => address).ToArray();
            Phase = int.Parse(phase.ToString());

            Task.Run((Action)Run);
        }

        public void Run()
        {
            long i = 0, inputCount = 0;

            while (_program[i] != 99)
            {
                if (_program[i].ToString().EndsWith("1"))
                {
                    var mode1 = _program[i].ToString().PadLeft(5, '0')[2];
                    var mode2 = _program[i].ToString().PadLeft(5, '0')[1];

                    var param1 = mode1 == '0' ? _program[_program[i + 1]] : _program[i + 1];
                    var param2 = mode2 == '0' ? _program[_program[i + 2]] : _program[i + 2];

                    _program[_program[i + 3]] = param1 + param2;
                    i += 4;
                }

                if (_program[i].ToString().EndsWith("2"))
                {
                    var mode1 = _program[i].ToString().PadLeft(5, '0')[2];
                    var mode2 = _program[i].ToString().PadLeft(5, '0')[1];

                    var param1 = mode1 == '0' ? _program[_program[i + 1]] : _program[i + 1];
                    var param2 = mode2 == '0' ? _program[_program[i + 2]] : _program[i + 2];

                    _program[_program[i + 3]] = param1 * param2;
                    i += 4;
                }

                if (_program[i] == 3)
                {
                    if (inputCount == 0)
                    {
                        _program[_program[i + 1]] = Phase;
                    }
                    else
                    {
                        while (Inputs.Count == 0)
                            Thread.Sleep(0);

                        _program[_program[i + 1]] = Inputs.Dequeue();
                    }
                    inputCount++;
                    i += 2;
                }

                if (_program[i].ToString().EndsWith("4"))
                {
                    var mode1 = _program[i].ToString().PadLeft(5, '0')[2];
                    var output = mode1 == '0' ? _program[_program[i + 1]] : _program[i + 1];
                    Outputs.Enqueue(output);
                    i += 2;
                }

                if (_program[i].ToString().EndsWith("5"))
                {
                    var mode1 = _program[i].ToString().PadLeft(5, '0')[2];
                    var mode2 = _program[i].ToString().PadLeft(5, '0')[1];

                    var param1 = mode1 == '0' ? _program[_program[i + 1]] : _program[i + 1];
                    var param2 = mode2 == '0' ? _program[_program[i + 2]] : _program[i + 2];

                    i = param1 != 0 ? param2 : i + 3;
                }

                if (_program[i].ToString().EndsWith("6"))
                {
                    var mode1 = _program[i].ToString().PadLeft(5, '0')[2];
                    var mode2 = _program[i].ToString().PadLeft(5, '0')[1];

                    var param1 = mode1 == '0' ? _program[_program[i + 1]] : _program[i + 1];
                    var param2 = mode2 == '0' ? _program[_program[i + 2]] : _program[i + 2];

                    i = param1 == 0 ? param2 : i + 3;
                }

                if (_program[i].ToString().EndsWith("7"))
                {
                    var mode1 = _program[i].ToString().PadLeft(5, '0')[2];
                    var mode2 = _program[i].ToString().PadLeft(5, '0')[1];

                    var param1 = mode1 == '0' ? _program[_program[i + 1]] : _program[i + 1];
                    var param2 = mode2 == '0' ? _program[_program[i + 2]] : _program[i + 2];

                    _program[_program[i + 3]] = param1 < param2 ? 1 : 0;
                    i += 4;
                }

                if (_program[i].ToString().EndsWith("8"))
                {
                    var mode1 = _program[i].ToString().PadLeft(5, '0')[2];
                    var mode2 = _program[i].ToString().PadLeft(5, '0')[1];

                    var param1 = mode1 == '0' ? _program[_program[i + 1]] : _program[i + 1];
                    var param2 = mode2 == '0' ? _program[_program[i + 2]] : _program[i + 2];

                    _program[_program[i + 3]] = param1 == param2 ? 1 : 0;
                    i += 4;
                }
            }

            Halted = true;
        }
    }
}