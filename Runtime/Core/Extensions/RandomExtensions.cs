using System;

namespace BaseTool
{
    public static class RandomExtensions
    {
        /// <summary>
        /// UnityEngine.Random.Range() equivalent.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Next(this Random random, float min, float max)
        {
            if (min == max) return min;

            if (min > max)
            {
                float tmp = min;
                min = max;
                max = tmp;
            }

            return (float)random.NextDouble() * Math.Abs(max - min) + min;
        }
    }
}
