using System;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTool
{
    public static class ListExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            return list[random];
        }

        public static T GetRandom<T>(this List<T> list, System.Random random)
        {
            int index = random.Next(0, list.Count);
            return list[index];
        }

        public static T ExtractRandom<T>(this List<T> list)
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            T val = list[random];
            list.RemoveAt(random);
            return val;
        }

        public static T ExtractRandom<T>(this List<T> list, System.Random random)
        {
            int index = random.Next(0, list.Count);
            T val = list[index];
            list.RemoveAt(index);
            return val;
        }

        public static void ForEach<T>(this List<T> list, Action<T, int> callback)
        {
            foreach (int i in 0..list.Count) callback?.Invoke(list[i], i);
        }
    }
}