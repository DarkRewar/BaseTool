using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTool.RNG
{
    [Serializable]
    public class Deck<T> : IEnumerable<T>, ICollection<T>
    {
        [HideInInspector]
        public PonderateRandom<T> Elements;

        public List<T> Cards;

        public T this[int index]
        {
            get => Cards[index];
            set => Cards[index] = value;
        } 
        
        public IEnumerator<T> GetEnumerator() => Cards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}