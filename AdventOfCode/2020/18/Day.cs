namespace AdventOfCode2020;
public static class Day18
{
    public static void Solve()
    {
        var input = File.ReadAllLines("AdventOfCode/2020/18/Input.txt");

        var result1 = input.Sum(line => Solve(line, EvalLeftToRight));
        var result2 = input.Sum(line => Solve(line, EvalAdditionFirst));

        Console.WriteLine($"Part 1: {result1}, Part 2: {result2}");

        long Solve(string expr, Func<string, long> evaluate)
        {
            while (expr.Contains('('))
            {
                int open = expr.LastIndexOf('(');
                int close = expr.IndexOf(')', open);
                string parentheses = expr.Substring(open, close - open + 1);
                expr = expr.Replace(parentheses, evaluate(parentheses[1..^1]).ToString());
            }

            return evaluate(expr);
        }

        long EvalLeftToRight(string expr)
        {
            while (expr.Contains(' '))
            {
                var items = expr.Split(' ').Take(3);
                expr = Combine(items.ToArray()).ToString() + expr[string.Join(' ', items).Length..];
            }

            return long.Parse(expr);
        }

        long EvalAdditionFirst(string expr)
        {
            while (expr.Contains('+'))
            {
                var split = expr.Split(' ');
                var operatorPos = split.ToList().IndexOf("+");
                var pre = split[..(operatorPos - 1)];
                var post = split[(operatorPos + 2)..];
                var items = split[(operatorPos - 1)..(operatorPos + 2)];
                expr = string.Join(' ', pre) + " " + Combine(items).ToString() + " " + string.Join(' ', post);
            }

            return EvalLeftToRight(expr.Trim());
        }

        static long Combine(string[] items) => items[1][0] switch
        {
            '+' => long.Parse(items[0]) + long.Parse(items[2]),
            '*' => long.Parse(items[0]) * long.Parse(items[2]),
            _ => throw new ArgumentException(string.Join(' ', items)),
        };
    }
}