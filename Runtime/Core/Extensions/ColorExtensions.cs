using UnityEngine;

namespace BaseTool
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Change la valeur rouge d'une couleur
        /// </summary>
        public static Color ChangeRed(this Color value, float red)
        {
            value.r = red;
            return value;
        }

        /// <summary>
        /// Change la valeur verte d'une couleur
        /// </summary>
        public static Color ChangeGreen(this Color value, float green)
        {
            value.g = green;
            return value;
        }
        /// <summary>
        /// Change la valeur bleu d'une couleur
        /// </summary>
        public static Color ChangeBlue(this Color value, float blue)
        {
            value.b = blue;
            return value;
        }
        /// <summary>
        /// Change la valeur alpha d'une couleur
        /// </summary>
        public static Color ChangeAlpha(this Color value, float alpha)
        {
            value.a = alpha;
            return value;
        }

        /// <summary>
        /// Convertit une couleur Unity en Hexadecimale.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHex(this Color value)
        {
            return string.Format(
                "#{0:X2}{1:X2}{2:X2}",
                (byte)(value.r * 255),
                (byte)(value.g * 255),
                (byte)(value.b * 255)
            );
        }

        /// <summary>
        /// Convertit une couleur Unity en Hexadecimale avec l'alpha.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToHexAlpha(this Color value)
        {
            return string.Format(
                "#{0:X2}{1:X2}{2:X2}{3:X2}",
                (byte)(value.r * 255),
                (byte)(value.g * 255),
                (byte)(value.b * 255),
                (byte)(value.a * 255)
            );
        }
    }
}
