using UnityEngine;

namespace BaseTool
{
    public static class MathUtils
    {
        /// <summary>
        /// The modulo function because using % causes troubles.
        /// If you want "-1 % 2", better using this method.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Modulo(int index, int count)
        {
            return (index % count + count) % count;
        }

        /// <summary>
        /// Work quite like the <see cref="UnityEngine.Mathf.Approximately(float, float)"/>
        /// method but allows a third arguments to change the tolerance.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Approximately(float a, float b, float tolerance = 0.001f)
        {
            return Mathf.Abs(b - a) < Mathf.Max(Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), tolerance);
        }
    }
}
