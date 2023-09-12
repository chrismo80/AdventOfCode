namespace AdventOfCode2022;

public static class Day25
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2022/25/Input.txt");

        Console.WriteLine(ConvertTo(input.Sum(snafu => ConvertFrom(snafu, "=-012", -2)), "=-012", -2));

        long ConvertFrom(string text, string baseString = "0123456789ABCDEF", int offset = 0)
        {
            long value = 0, power = 1;

            foreach (char c in text.Reverse())
            {
                value += power * (baseString.IndexOf(c) + offset);
                power *= baseString.Length;
            }

            return value;
        }

        string ConvertTo(long value, string baseString = "0123456789ABCDEF", int offset = 0)
        {
            string text = "";

            do
            {
                value -= offset;
                text = baseString[(int)(value % baseString.Length)] + text;
                value /= baseString.Length;
            }
            while (value > 0);

            return text;
        }
    }
}