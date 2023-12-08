namespace aoc_2022.Days.Dec07;

public class CamelCard
{
    private readonly long _weight = 100000000000000;

    public int CalculateWinnings(List<List<string>> handsAndBets)
    {
        var winnings = 0;
        var rankAndOrderedHands = RankHands(handsAndBets);
        int i = 1;
        foreach (var winning in rankAndOrderedHands.Select(rankedHand => rankedHand.Value.Item2))
        {
            winnings += winning * i;
            i++;
        }

        return winnings;
    }

    private SortedDictionary<long, (string, int)> RankHands(List<List<string>> handsAndBets)
    {
        SortedDictionary<long, (string, int)> memory = new SortedDictionary<long, (string, int)>();

        foreach (var handsAndBet in handsAndBets)
        {
            var hand = handsAndBet[0];
            var rank = GetRankValue(hand);
            memory.Add(rank, (hand, int.Parse(handsAndBet[1])));
        }

        return memory;
    }

    private long GetRankValue(string hand)
    {
        var cards = hand.ToCharArray().ToList();
        var cardCombinationRank = HasXOfAKind(5, cards) ?? HasXOfAKind(4, cards) ?? HasFullHouse(cards) ?? HasXOfAKind(3, cards) ?? HasTwoPairs(cards) ?? HasXOfAKind(2, cards) ?? GetHighestCard(cards);
        var cardValuesRank = GetValueRanking(cards);
        return cardCombinationRank + cardValuesRank;
    }

    private long GetValueRanking(List<char> cards)
    {
        long value = 0;
        var multiplier = 1000000;
        foreach (var card in cards)
        {
            value += GetCardValue(card) * multiplier;
            multiplier /= 20;
        }

        return value;
    }

    private int GetHighestCard(List<char> cards)
    {
        return cards.Max(GetCardValue);
    }

    private int GetCardValue(char c)
    {
        if (char.IsDigit(c)) return int.Parse(c.ToString());
        switch (c)
        {
            case 'A': return 14;
            case 'K': return 13;
            case 'Q': return 12;
            case 'J': return 11;
            case 'T': return 10;
        }

        throw new Exception("Something is wrong!");
    }


    private long? HasFullHouse(List<char> cards)
    {
        var groups = cards.GroupBy(i => i);
        var ordered = groups.OrderByDescending(s => s.Count());
        if (ordered.First().Count() == 3 && ordered.ElementAt(1).Count() == 2)
        {
            return (long)(3.5 * _weight) ;
        }

        return null;
    }
    
    private long? HasTwoPairs(List<char> cards)
    {
        var groups = cards.GroupBy(i => i);
        var ordered = groups.OrderByDescending(s => s.Count());
        if (ordered.First().Count() == 2 && ordered.ElementAt(1).Count() == 2)
        {
            return (long)(2.5 * _weight) ;
        }

        return null;
    }
    
    private long? HasXOfAKind(long x, List<char> cards)
    {
        var groups = cards.GroupBy(i => i);
        var maxLen = groups.OrderByDescending(s => s.Count()).First().Count();
        if (maxLen == x) return x*_weight;
        return null;
    }
}