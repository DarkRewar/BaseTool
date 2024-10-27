using UnityEngine;

namespace BaseTool.Samples.RNG.Poker
{
    public enum Suit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }

    public enum CardValue
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    public class Card : ScriptableObject
    {
        public Suit Suit;
        public CardValue Value;
        public Sprite Sprite;
    }
}