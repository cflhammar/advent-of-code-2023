namespace aoc_2022.Days.Dec07;

public class CamelCardWithJoker
{
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
            var handCombinationsWithJoker = GetPossibleCombinations(hand);

            (long rank, string cards) bestCombination = (0, "");
            long rank;
            if (handCombinationsWithJoker.Any())
            {
                foreach (var h in handCombinationsWithJoker)
                {
                    var tempRank = GetRankValue(h, hand);
                    if (tempRank > bestCombination.rank)
                        bestCombination = (tempRank, h);
                }

                rank = bestCombination.rank;
            }
            else // hand without jokers
            {
                rank = GetRankValue(hand, hand);
            }

            memory.Add(rank, (hand, int.Parse(handsAndBet[1])));
        }

        return memory;
    }

    // Assuming it is always best to set all jokers as the same value and never different! 
    private List<string> GetPossibleCombinations(string hand)
    {
        var combinations = new List<string>();
        var cards = hand.ToCharArray().ToList();

        var nonJokers = cards.Where(c => c != 'J').ToList();
        var numJokers = 5 - nonJokers.Count();
        
        if (numJokers > 0)
        {
            var distinct = nonJokers.Distinct().ToList();
            foreach (var c in distinct)
            {
                var tempNonJokers = hand.Replace('J', c);
                combinations.Add( new string(tempNonJokers.ToArray()));
            }
        }

        return combinations;
    }

    private long GetRankValue(string handWithoutJoker, string handWithJoker)
    {
        var cards = handWithoutJoker.ToList();
        var cardCombinationRank = HasXOfAKind(5, cards) ?? HasXOfAKind(4, cards) ?? HasFullHouse(cards) ?? 
            HasXOfAKind(3, cards) ?? HasTwoPairs(cards) ?? HasXOfAKind(2, cards) ?? GetHighestCard(cards);
        var cardValuesRank = GetValueRanking(handWithJoker.ToList());
        return cardCombinationRank + cardValuesRank;
    }

    private long GetValueRanking(List<char> cards)
    {
        long value = 0;
        long multiplier = 99999999;
        foreach (var card in cards)
        {
            value += GetCardValue(card) * multiplier;
            multiplier /= 21;
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
            case 'J': return 1;
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
            return 350000000000000;
        }

        return null;
    }
    
    private long? HasTwoPairs(List<char> cards)
    {
        var groups = cards.GroupBy(i => i);
        var ordered = groups.OrderByDescending(s => s.Count());
        if (ordered.First().Count() == 2 && ordered.ElementAt(1).Count() == 2)
        {
            return 250000000000000;
        }

        return null;
    }
    
    private long? HasXOfAKind(long x, List<char> cards)
    {
        var groups = cards.GroupBy(i => i);
        var maxLen = groups.OrderByDescending(s => s.Count()).First().Count();
        if (maxLen == x) return x*100000000000000;
        return null;
    }
}