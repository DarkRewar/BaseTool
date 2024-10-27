using System;
using BaseTool.RNG;
using UnityEditor;
using UnityEngine;

namespace BaseTool.Samples.RNG.Poker
{
    public class DeckBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _handTransform;

        [SerializeField] private Deck<Card> _deck;

        private void Awake()
        {
            Fill();
        }

        public void Fill() => _deck.Fill();

        public Card Draw() => _deck.Draw();

#if UNITY_EDITOR

        [ContextMenu("Generate")]
        private void Generate()
        {
            for (int i = 0; i < 4; i++)
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    var card = ScriptableObject.CreateInstance<Card>();
                    card.Value = value;
                    card.Suit = (Suit) i;
                    _deck.Add(card);
                    AssetDatabase.CreateAsset(card,
                        $"Assets/Samples/RNG/Poker/Cards/{card.Suit}_{card.Value}.asset");
                }
            }
        }

        [ContextMenu("Populate")]
        public void Populate()
        {
            var cards = AssetDatabase.FindAssets("t:Card");
            foreach (var guid in cards)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                Card card = AssetDatabase.LoadAssetAtPath<Card>(path);
                _deck.SetWeight(card, 1);
            }
        }

#endif
    }
}