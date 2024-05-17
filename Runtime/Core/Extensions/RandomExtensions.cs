using System;

namespace BaseTool
{
    public static class RandomExtensions
    {
        /// <summary>
        /// <see cref="UnityEngine.Random.Range(float, float)"/> equivalent.
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
                (min, max) = (max, min);
            }

            return (float)random.NextDouble() * Math.Abs(max - min) + min;
        }
    }
}
