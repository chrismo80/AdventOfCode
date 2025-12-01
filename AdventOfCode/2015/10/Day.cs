namespace AdventOfCode2015;

public static class Day10
{
	public static IEnumerable<object> Solve(string input)
	{
		var sequence = input;

		for (var i = 0; i < 50; i++)
		{
			var builder = new System.Text.StringBuilder();
			var number = sequence[0];
			var count = 0;

			foreach (var letter in sequence)
			{
				if (letter != number)
				{
					builder.Append(count).Append(number);
					number = letter;
					count = 0;
				}

				count++;
			}

			sequence = builder.Append(count).Append(number).ToString();
		}

		yield return sequence.Length;
	}
}