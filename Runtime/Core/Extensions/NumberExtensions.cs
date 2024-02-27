using System;
using System.Linq;

namespace BaseTool
{
    public static class NumberExtensions
    {
        #region INT EXTENSIONS

        #region IN

        /// <summary>
        /// Vérifie si le nombre est l'un de ceux comparés.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="comp">Les </param>
        /// <returns></returns>
        public static bool IsIn(this int number, params int[] comp)
        {
            return comp.Contains(number);
        }

        #endregion

        #region BETWEEN

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle.
        /// Calcul : a <= origin && origin <= b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetween(this int origin, int a, int b)
        {
            return a <= origin && origin <= b;
        }

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle.
        /// Calcul : a <= origin && origin <= b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetween(this int origin, float a, float b)
        {
            return a <= origin && origin <= b;
        }

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle exclusif.
        /// Calcul : a < origin && origin < b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetweenExclusive(this int origin, int a, int b)
        {
            return a < origin && origin < b;
        }

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle exclusif.
        /// Calcul : a < origin && origin < b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetweenExclusive(this int origin, float a, float b)
        {
            return a < origin && origin < b;
        }

        #endregion

        #endregion

        #region FLOAT EXTENSIONS

        #region IN

        /// <summary>
        /// Vérifie si le nombre est l'un de ceux comparés.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="comp">Les </param>
        /// <returns></returns>
        public static bool IsIn(this float number, params int[] comp)
        {
            return comp.Contains((int)Math.Round(number));
        }

        /// <summary>
        /// Vérifie si le nombre est l'un de ceux comparés.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="comp">Les </param>
        /// <returns></returns>
        public static bool IsIn(this float number, params float[] comp)
        {
            return comp.Contains(number);
        }

        #endregion

        #region BETWEEN

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle.
        /// Calcul : a <= origin && origin <= b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetween(this float origin, int a, int b)
        {
            return a <= origin && origin <= b;
        }

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle.
        /// Calcul : a <= origin && origin <= b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetween(this float origin, float a, float b)
        {
            return a <= origin && origin <= b;
        }

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle exclusif.
        /// Calcul : a < origin && origin < b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetweenExclusive(this float origin, int a, int b)
        {
            return a < origin && origin < b;
        }

        /// <summary>
        /// Vérifie si le nombre se situe dans cet intervalle exclusif.
        /// Calcul : a < origin && origin < b
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsBetweenExclusive(this float origin, float a, float b)
        {
            return a < origin && origin < b;
        }

        #endregion

        #endregion
    }
}
