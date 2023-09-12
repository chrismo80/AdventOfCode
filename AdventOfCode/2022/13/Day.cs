namespace AdventOfCode2022;

public static class Day13
{
    public static void Solve()
    {
        var packets = File.ReadAllLines("AdventOfCode/2022/13/Input.txt")
            .Where(l => l != "").ToList();

        var validPackets = packets.Chunk(2)
            .Select((pair, i) => Compare(pair[0], pair[1]) == -1 ? i + 1 : 0)
            .Where(i => i != 0);

        Console.WriteLine($"Part 1: {validPackets.Sum()}");

        var part2Packets = packets;

        part2Packets.Add("[[2]]");
        part2Packets.Add("[[6]]");
        part2Packets.Sort(Compare);
        var d1 = part2Packets.IndexOf("[[2]]") + 1;
        var d2 = part2Packets.IndexOf("[[6]]") + 1;

        Console.WriteLine($"Part 2: {d1 * d2}");

        int Compare(string p1, string p2)
        {
            while (p1.Length > 0 && p2.Length > 0)
            {
                var t1 = GetToken(p1);
                var t2 = GetToken(p2);

                if (t1.token.Length == 0) return -1;
                if (t2.token.Length == 0) return 1;

                if (t1.token.All(char.IsDigit) && t2.token.All(char.IsDigit))
                {
                    var n1 = int.Parse(t1.token);
                    var n2 = int.Parse(t2.token);

                    if (n1 < n2) return -1;
                    if (n1 > n2) return 1;
                }
                else
                {
                    if (t1.token[0] == '[' ^ t2.token[0] == '[') // XOR
                    {
                        t1.token = Square(t1.token);
                        t2.token = Square(t2.token);
                    }

                    var compare = Compare(t1.token[1..^1], t2.token[1..^1]);
                    if (compare != 0) return compare;
                }

                p1 = t1.rest;
                p2 = t2.rest;
            }

            return p2.Length > 0 ? -1 : p1.Length > 0 ? 1 : 0;
        }

        (string token, string rest) GetToken(string s)
        {
            if (char.IsDigit(s[0]))
            {
                var token = new string(s.TakeWhile(char.IsDigit).ToArray());
                var next = token.Length == s.Length ? token.Length : token.Length + 1;
                return (token, s[next..]); // Skip(next)
            }

            int depth = 0;
            int pos = 0;

            while (!(depth == 0 && (pos == s.Length || s[pos] == ',')))
            {
                if (s[pos] == '[') depth++;
                if (s[pos] == ']') depth--;
                pos++;
            }

            return pos == s.Length ? (s, "") : (s[..pos], s[(pos + 1)..]);
        }

        string Square(string s) => s.All(char.IsDigit) ? $"[{s}]" : s;
    }
}