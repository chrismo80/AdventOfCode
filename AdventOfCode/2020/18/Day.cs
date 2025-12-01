using AdventOfCode;

namespace AdventOfCode2020;

public static class Day18
{
	public static IEnumerable<object> Solve(string input)
	{
		var lines = input.ToArray<string>("\n");

		yield return lines.Sum(line => Solve(line, EvalLeftToRight));
		yield return lines.Sum(line => Solve(line, EvalAdditionFirst));

		long Solve(string expr, Func<string, long> evaluate)
		{
			while (expr.Contains('('))
			{
				var open = expr.LastIndexOf('(');
				var close = expr.IndexOf(')', open);
				var parentheses = expr.Substring(open, close - open + 1);
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

		static long Combine(string[] items)
		{
			return items[1][0] switch
			{
				'+' => long.Parse(items[0]) + long.Parse(items[2]),
				'*' => long.Parse(items[0]) * long.Parse(items[2]),
				_ => throw new ArgumentException(string.Join(' ', items))
			};
		}
	}
}