using UnityEngine;

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

    Jack = 21,
    Queen = 22,
    King = 23
}

public class Card : ScriptableObject
{
    public Suit Suit;
    public CardValue Value;
}