namespace AdventOfCode2015;

using System.Text;
using System.Security.Cryptography;

public static class Day4
{
	public static IEnumerable<object> Solve(string input)
	{
		yield return Enumerable.Range(0, 100000000).First(i => GetMD5(input + i).StartsWith("00000"));
		yield return Enumerable.Range(0, 100000000).First(i => GetMD5(input + i).StartsWith("000000"));

		static string GetMD5(string text)
		{
			return Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(text)));
		}
	}
}