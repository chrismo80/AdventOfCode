namespace Language;

using GTranslate.Translators;

public static class Language
{
    public static void Translate()
    {
        var translator = new GTranslate.Translators.BingTranslator();

        var texts = new string[]
        {
             "hello world",
             "timeout pos2",
             "not enabled",
             "com error",
        };


        foreach (var text in texts)
            Console.WriteLine(text + " = " + translator.TranslateAsync(text, "de").GetAwaiter().GetResult().Translation);
    }
}