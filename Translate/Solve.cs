namespace Language;

public static class Language
{
    public static void Translate()
    {
        var translator = new GTranslate.Translators.BingTranslator();

        var texts = System.IO.File.ReadAllLines("Translate/Cp.csv")
            .Where(line => line.StartsWith("Stat.Errors"))
            .Select(line => line.Split(';').First().TranslateDefault());

        foreach (var text in texts)
            Console.WriteLine(text + " = " + translator.TranslateAsync(text, "de").GetAwaiter().GetResult().Translation);
    }

    private static string TranslateDefault(this string name) =>
        string.Join(" - ", name.Split('.').Select(word => word.MakeReadable()).Skip(2));

    private static string MakeReadable(this string word) // "NotEnabled" -> "not enabled"
    {
        foreach (char letter in word.Where(c => char.IsUpper(c)))
            word = word.Replace(letter.ToString(), $" {letter.ToString().ToLowerInvariant()}");

        return word.Trim();
    }
}