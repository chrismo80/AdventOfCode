namespace AdventOfCode2020;

using System.Text.RegularExpressions;

public static class Day4
{
	public static IEnumerable<object> Solve(string input)
	{
		var required = new string[] { "byr:", "eyr:", "iyr:", "hgt:", "hcl:", "ecl:", "pid:" };
		var allFieldsPresent = input.Split("\n\n").Where(passport => required.All(field => passport.Contains(field)));

		var passports = allFieldsPresent.Select(p => p.Split(' ', '\n'));
		var allFieldsValid = passports.Where(passport => required.All(field => passport.HasValid(field)));

		yield return allFieldsPresent.Count();
		yield return allFieldsValid.Count();
	}

	private static bool HasValid(this string[] passport, string field)
	{
		var data = passport.First(p => p.StartsWith(field)).Split(':');
		var eyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

		switch (data[0])
		{
			case "byr":
				if (int.Parse(data[1]) < 1920 || int.Parse(data[1]) > 2002) return false;
				break;
			case "eyr":
				if (int.Parse(data[1]) < 2020 || int.Parse(data[1]) > 2030) return false;
				break;
			case "iyr":
				if (int.Parse(data[1]) < 2010 || int.Parse(data[1]) > 2020) return false;
				break;
			case "ecl":
				if (!eyeColors.Contains(data[1])) return false;
				break;
			case "hcl":
				if (!new Regex(@"^#(?:[0-9a-fA-F]{3}){1,2}$").Match(data[1]).Success) return false;
				break;
			case "pid":
				if (!new Regex(@"^\d{9}$").Match(data[1]).Success) return false;
				break;
			case "hgt":
				int height;
				if (!int.TryParse(data[1].SkipLast(2).ToArray(), out height)) return false;
				var unit = new string(data[1].TakeLast(2).ToArray());
				if (unit == "cm" && (height < 150 || height > 193)) return false;
				if (unit == "in" && (height < 59 || height > 76)) return false;
				break;
		}

		return true;
	}
}