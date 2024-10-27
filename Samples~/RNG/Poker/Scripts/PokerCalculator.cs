using System.Collections.Generic;
using System.Linq;

namespace BaseTool.Samples.RNG.Poker
{
    public enum Hand
    {
        HighCard,
        Pair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        StraightFlushRoyal,
    }

    public struct HandResult
    {
        public Hand Hand;
        public List<Card> Cards;
    }

    public static class PokerCalculator
    {
        public static HandResult GetHandResult(List<Card> cards)
        {
            var hand = GetHand(cards);
            switch (hand)
            {
                case Hand.StraightFlushRoyal:
                case Hand.StraightFlush:
                case Hand.Straight:
                case Hand.FullHouse:
                case Hand.Flush:
                    return new HandResult
                    {
                        Hand = hand,
                        Cards = cards
                    };
                case Hand.FourOfAKind:
                    var four = cards.GroupBy(c => c.Value).First(p => p.Count() == 4);
                    return new HandResult
                    {
                        Hand = hand,
                        Cards = four.ToList()
                    };
                case Hand.ThreeOfAKind:
                    var three = cards.GroupBy(c => c.Value).First(p => p.Count() == 3);
                    return new HandResult
                    {
                        Hand = hand,
                        Cards = three.ToList()
                    };
                case Hand.TwoPairs:
                    var pairs = cards.GroupBy(c => c.Value)
                        .Where(p => p.Count() == 2)
                        .SelectMany(p => p, (_, v) => v);
                    return new HandResult
                    {
                        Hand = hand,
                        Cards = pairs.ToList()
                    };
                case Hand.Pair:
                    var pair = cards.GroupBy(c => c.Value).First(p => p.Count() == 2);
                    return new HandResult
                    {
                        Hand = hand,
                        Cards = pair.ToList()
                    };
                case Hand.HighCard:
                default:
                    return new HandResult
                    {
                        Hand = Hand.HighCard,
                        Cards = new List<Card> {cards.OrderByDescending(c => c.Value == CardValue.Ace ? 999 : (int)c.Value).First()}
                    };
            }
        }

        public static Hand GetHand(List<Card> cards)
        {
            // Calc preparation
            var suitGroups = cards.GroupBy(c => c.Suit);
            var valueGroups = cards.GroupBy(c => c.Value);
            var orderedCards = cards.OrderBy(c => c.Value == CardValue.Ace ? 100 : (int) c.Value);

            // Value occurrence count
            int maxSameCards = valueGroups.Max(g => g.Count());

            // Highest calc
            Card highestCard = orderedCards.Last();

            // Pairs calc
            int numberOfPairs = valueGroups.Count(g => g.Count() >= 2);

            // Flush calc
            bool isFlush = suitGroups.Count() == 1;

            // Straight calc
            bool isStraight = true;
            int currentValue = -1;
            foreach (Card card in orderedCards)
            {
                if (currentValue != -1)
                {
                    if ((int) card.Value - currentValue != 1)
                    {
                        isStraight = false;
                        break;
                    }
                }

                currentValue = (int) card.Value;
            }

            // Result parsing
            if (isStraight && isFlush && highestCard.Value == CardValue.Ace)
                return Hand.StraightFlushRoyal;
            if (isStraight && isFlush)
                return Hand.StraightFlush;
            if (maxSameCards == 4)
                return Hand.FourOfAKind;
            if (maxSameCards == 3 && numberOfPairs == 2)
                return Hand.FullHouse;
            if (isFlush)
                return Hand.Flush;
            if (isStraight)
                return Hand.Straight;
            if (maxSameCards == 3)
                return Hand.ThreeOfAKind;
            if (numberOfPairs == 2)
                return Hand.TwoPairs;
            if (numberOfPairs == 1)
                return Hand.Pair;

            return Hand.HighCard;
        }
    }
}