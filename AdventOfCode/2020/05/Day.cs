using AdventOfCode;

namespace AdventOfCode2020;

public static class Day5
{
	public static IEnumerable<object> Solve(string input)
	{
		var seatIds = input.Lines().Select(code => 8 * Position(code.Take(7), 'B', 128) + Position(code.Skip(7), 'R', 8));

		yield return seatIds.Max();
		yield return FirstEmpty(seatIds.Order());

		static int Position(IEnumerable<char> code, char direction, int end)
		{
			return code.Aggregate((Min: 0, Max: end), (last, current) =>
			(
				Min: current != direction ? last.Min : last.Min + (last.Max - last.Min) / 2,
				Max: current == direction ? last.Max : last.Min + (last.Max - last.Min) / 2
			)).Min;
		}

		static int FirstEmpty(IEnumerable<int> seatIds)
		{
			return seatIds.Aggregate((Id: seatIds.First(), Empty: 0), (last, current) =>
			(
				Id: current,
				Empty: current - last.Id > 1 && last.Empty == 0 ? last.Id + 1 : last.Empty
			)).Empty;
		}
	}
}