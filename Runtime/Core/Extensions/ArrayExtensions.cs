using System;

namespace BaseTool
{
    public static class ArrayExtensions
    {
        public static void ForEach<T>(this T[] array, Action<T> callback)
        {
            for (int i = 0; i < array.Length; i++) { callback.Invoke(array[i]); }
        }

        public static void ForEach<T>(this T[] array, Action<T, int> callback)
        {
            for (int i = 0; i < array.Length; i++) { callback.Invoke(array[i], i); }
        }
    }
}