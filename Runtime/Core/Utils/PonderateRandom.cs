using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaseTool
{
    public struct PonderateEntry<T>
    {
        public T Value;
        public float Weight;

        internal Vector2 Range;
    }

    [Serializable]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#ponderaterandom")]
    public class PonderateRandom<T> : IEnumerable<PonderateEntry<T>>
    {
        private List<PonderateEntry<T>> _entries;

        public float TotalWeight { get; private set; } = 0;

        public PonderateRandom()
        {
            _entries = new List<PonderateEntry<T>>();
        }

        public IEnumerator<BaseTool.PonderateEntry<T>> GetEnumerator() => _entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _entries.GetEnumerator();

        public void Add(T value) => Add(value, 1);

        public void Add(T value, float weight)
        {
            _entries.Add(new PonderateEntry<T>
            {
                Value = value,
                Weight = weight,
                Range = new(TotalWeight, (TotalWeight + weight))
            });
            TotalWeight += weight;
        }

        public void Remove(T value)
        {
            _entries.RemoveAll(e => e.Value.Equals(value));
            RecalculateRanges();
        }

        public void SetWeight(T value, float newWeight)
        {
            Remove(value);
            Add(value, newWeight);
        }

        private void RecalculateRanges()
        {
            TotalWeight = 0;
            foreach (int i in 0.._entries.Count)
            {
                var entry = _entries[i];
                entry.Range.x = TotalWeight;
                entry.Range.y = TotalWeight + entry.Weight;
                _entries[i] = entry;
                TotalWeight += entry.Weight;
            }
        }

        public T Get() => Get(UnityEngine.Random.Range(0, TotalWeight));

        public T Get(System.Random random) => Get(random.Next(0, TotalWeight));

        private T Get(float rand)
        {
            var increment = TotalWeight - rand < rand ? -1 : 1;
            int i = TotalWeight - rand < rand ? _entries.Count - 1 : 0;
            for (; i.IsBetween(0, _entries.Count - 1); i += increment)
            {
                if (rand.IsBetween(_entries[i].Range))
                    return _entries[i].Value;
            }
            throw new IndexOutOfRangeException("Impossible to find an entry for ponderate random.");
        }

        public Dictionary<T, float> ToDictionary() => _entries.ToDictionary(
            entry => entry.Value,
            entry => entry.Weight);
    }
}
