using System;
using System.Collections.Generic;
using UnityEngine;

namespace BaseTool
{
    public static class ListExtensions
    {
        /// <summary>
        /// Get a random element in the list,
        /// based on the <see cref="UnityEngine.Random"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this List<T> list)
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            return list[random];
        }

        /// <summary>
        /// Get a random element in the list, based on <see cref="System.Random"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this List<T> list, System.Random random)
        {
            int index = random.Next(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// Extract a random element from the list by removing it
        /// and returns it, based on the <see cref="UnityEngine.Random"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T ExtractRandom<T>(this List<T> list)
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            T val = list[random];
            list.RemoveAt(random);
            return val;
        }

        /// <summary>
        /// Extract a random element from the list by removing it
        /// and returns it, based on the <see cref="System.Random"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T ExtractRandom<T>(this List<T> list, System.Random random)
        {
            int index = random.Next(0, list.Count);
            T val = list[index];
            list.RemoveAt(index);
            return val;
        }

        /// <summary>
        /// Iterate through the list and invoke the callback for each element, with its index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="callback"></param>
        public static void ForEach<T>(this List<T> list, Action<T, int> callback)
        {
            foreach (int i in 0..list.Count) callback?.Invoke(list[i], i);
        }
        
        /// <summary>
        /// Shuffle list elements using the <see cref="UnityEngine.Random"/> class.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = UnityEngine.Random.Range(0, n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
        
        /// <summary>
        /// Shuffle list elements using the <see cref="System.Random"/> class.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="random"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list, System.Random random)  
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = random.Next(n + 1);  
                (list[k], list[n]) = (list[n], list[k]);
            }  
        }
    }
}