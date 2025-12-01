using AdventOfCode;

namespace AdventOfCode2018;

public static class Day7
{
	public static IEnumerable<object> Solve(string input)
	{
		var data = input.Lines().Select(row => row.Split(' '))
			.Select(words => (Step: words[1][0], Before: words[7][0]));

		var specs = data.Select(i => i.Before).Distinct()
			.Select(b => (Step: b, Required: data.Where(i => i.Before == b).Select(i => i.Step)));

		var done = new List<char>();
		var available = data.Select(x => x.Step).Except(specs.Select(specs => specs.Step)).Order().ToList();

		const int workers = 2;
		var duration = 0;

		while (available.Any())
		{
			done.Add(available[0]);

			duration += 60 + (done.Last() - '@');

			available.AddRange(specs.Where(spec => spec.Required.All(required => done.Contains(required)))
				.Select(spec => spec.Step));
			available = available.Except(done).Distinct().Order().ToList();
		}

		yield return new string(done.ToArray());
		yield return duration;
	}
}