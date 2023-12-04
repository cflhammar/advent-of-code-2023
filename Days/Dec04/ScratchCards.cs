using System.Text.RegularExpressions;

namespace aoc_2022.Days.Dec04;

public class ScratchCards
{
    public static int CountPoints(List<List<string>> cards)
    {
        double sum = 0;
        foreach (var card in cards)
        {
            var hits = CountWinningNumbers(card);
            if (hits != 0) sum += Math.Pow(2, (double) hits - 1);
        }

        return (int)sum;
    }
    
    public static int CountCards(List<List<string>> cards)
    {
        List<int> cardCounter = Enumerable.Repeat(1, cards.Count+1).ToList();

        cards.Insert(0, null!);
        for (int i = 1; i < cards.Count; i++)
        {
            var hits = CountWinningNumbers(cards[i]);

            for (int k = 1; k <= hits; k++)
            {
                cardCounter[i + k] += cardCounter[i];
            }
        }

        return cardCounter.Sum()-1;
    }


    private static int CountWinningNumbers(List<string> card)
    {
        var winningNumbers = Regex.Split(card[0], "\\s+");
        var numbers = Regex.Split(card[1], "\\s+");

        var winners = winningNumbers.Intersect(numbers).ToList();
        return winners.Count;
    }
}