using System;
using System.Collections.Generic;
using BaseTool;
using BaseTool.RNG;
using UnityEditor;
using UnityEngine;

public class DeckBehaviour : MonoBehaviour
{
    [SerializeField] private List<Card> _list = new List<Card>();

    [SerializeField] private PonderateRandom<Card> _cards;

    [SerializeField] private Deck<Card> _deck;

    void Start()
    {
    }

    void Update()
    {
    }

    [ContextMenu("Generate")]
    private void Generate()
    {
        for (int i = 0; i < 4; i++)
        {
            foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
            {
                var card = ScriptableObject.CreateInstance<Card>();
                card.Value = value;
                card.Suit = (Suit)i;
                _cards.Add(card);
                _deck.Add(card);
                AssetDatabase.CreateAsset(card, $"Assets/Samples/RNG/Poker/Cards/{card.Suit}_{card.Value}.asset");
            }
        }
    }
}