using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = System.Random;

namespace BaseTool.RNG
{
    [Serializable]
    public class Deck<T> : PonderateRandom<T>
    {
        private readonly List<T> _cards = new();

        [CanBeNull] private readonly Random _random;

        public T this[int index] => _cards[index];
        
        public Deck() {}
        
        public Deck(int seed) : this(new Random(seed)) {}

        public Deck(Random random)
        {
            _random = random;
        }

        public new void Add(T card)
        {
            base.Add(card);
            _cards.Add(card);
        }

        /// <summary>
        /// Fill the deck based on the card quantity.
        /// </summary>
        public void Fill()
        {
            _cards.Clear();
            foreach (var entry in _entries)
            {
                for (int i = 0; i < entry.Weight; i++)
                {
                    _cards.Add(entry.Value);
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            if(_random == null) _cards.Shuffle();
            else _cards.Shuffle(_random);
        } 

        public T Peek() => _cards.Count > 0 ? _cards[0] : default;
        
        public List<T> Peek(int number) => _cards.Count > 0 ? _cards.Take(Mathf.Min(number, _cards.Count)).ToList() : default;

        public T Draw()
        {
            if(_cards.Count == 0) return default;
            var peek = Peek();
            _cards.RemoveAt(0);
            return peek;
        }
    }
}