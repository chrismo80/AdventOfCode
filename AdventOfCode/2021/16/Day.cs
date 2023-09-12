namespace AdventOfCode2021;
using Extensions;
public static class Day16
{
    public static void Solve()
    {
        var input = File.ReadAllText("AdventOfCode/2021/16/Input.txt");

        var digits = string.Concat(input.Select(hexChar =>
            Convert.ToString(Convert.ToInt32(hexChar.ToString(), 16), 2).PadLeft(4, '0')));

        int pointer = 0;
        var root = new Packet(digits, ref pointer);

        Console.WriteLine($"Part 1: {root.VersionSum}, Part 2: {root.Value}");
    }

    public class Packet
    {
        public int Version, Type;
        public long Value;
        public int VersionSum => Version + Children.Sum(c => c.VersionSum);
        public List<Packet> Children = new();

        public Packet(string digits, ref int i)
        {
            Version = Convert.ToInt32(digits[i..(i += 3)], 2);
            Type = Convert.ToInt32(digits[i..(i += 3)], 2);

            if (Type != 4)
                ParseChildren(digits, ref i);

            Value = Type switch
            {
                0 => Children.Sum(c => c.Value),
                1 => Children.Product(c => c.Value),
                2 => Children.Min(c => c.Value),
                3 => Children.Max(c => c.Value),
                4 => GetLiteralValue(digits, ref i),
                5 => Children[0].Value > Children[1].Value ? 1 : 0,
                6 => Children[0].Value < Children[1].Value ? 1 : 0,
                7 => Children[0].Value == Children[1].Value ? 1 : 0,
                _ => 0,
            };
        }

        private static long GetLiteralValue(string digits, ref int i)
        {
            string value = "";

            while (digits[i++] == '1')
                value += digits[i..(i += 4)];
            value += digits[i..(i += 4)];

            return Convert.ToInt64(value, 2);
        }

        private void ParseChildren(string digits, ref int i)
        {
            if (digits[i++] == '1')
            {
                int packetsCount = Convert.ToInt32(digits[i..(i += 11)], 2);

                while (packetsCount-- > 0)
                    Children.Add(new Packet(digits, ref i));
            }
            else
            {
                int packetsEnd = Convert.ToInt32(digits[i..(i += 15)], 2) + i;

                while (i < packetsEnd)
                    Children.Add(new Packet(digits, ref i));
            }
        }
    }
}