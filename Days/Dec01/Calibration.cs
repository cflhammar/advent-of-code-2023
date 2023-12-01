namespace aoc_2022.Days.Dec01;

public class Calibration
{
    private List<string> digits = new()
        { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    private List<string> wordDigits = new()
        { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    public int FindDigit(List<string> entries, bool lookForWordsAlso)
    {
        var sum = 0;

        foreach (var entry in entries)
        {
            var firstDigit = FindFirstDigit(entry, lookForWordsAlso);
            var lastDigit = FindLastDigit(entry, lookForWordsAlso);

            sum += int.Parse($"{firstDigit}{lastDigit}");
        }

        return sum;
    }

    private int FindFirstDigit(string entry, bool toggle)
    {
        var firstDigit = 0;
        var firstIndex = 999999;
        for (int i = 1; i < 10; i++)
        {
            var matchDigit = digits[i - 1];
            var matchWord = wordDigits[i - 1];


            var digitIndex = entry.IndexOf(matchDigit, StringComparison.Ordinal);
            var wordIndex = entry.IndexOf(matchWord, StringComparison.Ordinal);

            if (digitIndex > -1 && digitIndex < firstIndex)
            {
                firstIndex = digitIndex;
                firstDigit = i;
            }

            if (toggle && wordIndex > -1 && wordIndex < firstIndex)
            {
                firstIndex = wordIndex;
                firstDigit = i;
            }
        }

        return firstDigit;
    }

    private int FindLastDigit(string entry, bool toggle)
    {
        var lastDigit = 0;
        var lastIndex = -1;
        for (int i = 1; i < 10; i++)
        {
            var matchDigit = digits[i - 1];
            var matchWord = wordDigits[i - 1];


            var digitIndex = entry.LastIndexOf(matchDigit, StringComparison.Ordinal);
            var wordIndex = entry.LastIndexOf(matchWord, StringComparison.Ordinal);

            if (digitIndex > -1 && digitIndex > lastIndex)
            {
                lastIndex = digitIndex;
                lastDigit = i;
            }

            if (toggle && wordIndex > -1 && wordIndex > lastIndex)
            {
                lastIndex = wordIndex;
                lastDigit = i;
            }
        }

        return lastDigit;
    }
}