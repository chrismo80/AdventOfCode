namespace AdventOfCode2015
{
    public static class Day5
    {
        public static void Solve()
        {
            var input = File.ReadAllLines("AdventOfCode/2015/05/Input.txt");

            var result1 = input
                .Count(word => HasNoInvalid(word) && HasCharsInARow(word) && Has3Vowels(word));

            var result2 = input
                .Count(word => ContainsRepeatedInBetween(word) && ContainsMultiplePairs(word));

            Console.WriteLine($"Part 1: {result1} (236), Part 2: {result2} (51)");

            bool ContainsRepeatedInBetween(string word) => word.SkipWhile((_, i) =>
                !word.Skip(i).Take(1).SequenceEqual(word.Skip(i + 2).Take(1))
                ).Any();

            bool ContainsMultiplePairs(string word) => word.SkipWhile((_, i) =>
                !new string(word.Skip(i + 2).ToArray()).Contains(new string(word.Skip(i).Take(2).ToArray()))
                ).Any();

            bool HasNoInvalid(string word) =>
                !word.Contains("ab") &&
                !word.Contains("cd") &&
                !word.Contains("pq") &&
                !word.Contains("xy");

            bool HasCharsInARow(string word) => word.SkipWhile((_, i) =>
                !word.Skip(i).Take(1).SequenceEqual(word.Skip(i + 1).Take(1))
                ).Any();

            bool Has3Vowels(string word) => (
                word.Count(c => c == 'a') +
                word.Count(c => c == 'e') +
                word.Count(c => c == 'i') +
                word.Count(c => c == 'o') +
                word.Count(c => c == 'u')) >= 3;
        }
    }
}