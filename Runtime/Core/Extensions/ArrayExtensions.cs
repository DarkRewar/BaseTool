using System;

namespace BaseTool
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Get a random element in the array,
        /// based on the <see cref="UnityEngine.Random"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this T[] array)
        {
            int random = UnityEngine.Random.Range(0, array.Length);
            return array[random];
        }

        /// <summary>
        /// Get a random element in the array, based on <see cref="System.Random"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this T[] array, System.Random random)
        {
            int index = random.Next(0, array.Length);
            return array[index];
        }

        /// <summary>
        /// Iterate through the array and invoke the callback for each element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="callback"></param>
        public static void ForEach<T>(this T[] array, Action<T> callback)
        {
            for (int i = 0; i < array.Length; i++) { callback.Invoke(array[i]); }
        }

        /// <summary>
        /// Iterate through the array and invoke the callback for each element, with its index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="callback"></param>
        public static void ForEach<T>(this T[] array, Action<T, int> callback)
        {
            for (int i = 0; i < array.Length; i++) { callback.Invoke(array[i], i); }
        }
    }
}