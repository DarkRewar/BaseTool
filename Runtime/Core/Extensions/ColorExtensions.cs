using UnityEngine;

namespace BaseTool
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Change the color red value and returns the new color.
        /// </summary>
        /// <returns>The altered color with the red changed</returns>
        public static Color ChangeRed(this Color value, float red)
        {
            value.r = red;
            return value;
        }

        /// <summary>
        /// Change the color green value and returns the new color.
        /// </summary>
        /// <returns>The altered color with the green changed</returns>
        public static Color ChangeGreen(this Color value, float green)
        {
            value.g = green;
            return value;
        }
        /// <summary>
        /// Change the color blue value and returns the new color.
        /// </summary>
        /// <returns>The altered color with the blue changed</returns>
        public static Color ChangeBlue(this Color value, float blue)
        {
            value.b = blue;
            return value;
        }
        /// <summary>
        /// Change the color alpha value and returns the new color.
        /// </summary>
        /// <returns>The altered color with the alpha changed</returns>
        public static Color ChangeAlpha(this Color value, float alpha)
        {
            value.a = alpha;
            return value;
        }

        /// <summary>
        /// Convert a <see cref="Color"/> to hex (e.g. #FF00FF).
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The hex color (in uppercase)</returns>
        public static string ToHex(this Color value)
        {
            return string.Format(
                "#{0:X2}{1:X2}{2:X2}",
                (byte)Mathf.Round(value.r * 255),
                (byte)Mathf.Round(value.g * 255),
                (byte)Mathf.Round(value.b * 255)
            );
        }

        /// <summary>
        /// Convert a <see cref="Color"/> to hex including alpha (e.g. #FF00FFDD).
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The hex color and its alpha (in uppercase)</returns>
        public static string ToHexAlpha(this Color value)
        {
            return string.Format(
                "#{0:X2}{1:X2}{2:X2}{3:X2}",
                (byte)Mathf.Round(value.r * 255),
                (byte)Mathf.Round(value.g * 255),
                (byte)Mathf.Round(value.b * 255),
                (byte)Mathf.Round(value.a * 255)
            );
        }
    }
}
