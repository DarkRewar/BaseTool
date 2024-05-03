using System.Text;

namespace BaseTool
{
    /// <summary>
    /// StringExtensions adds many extensions for string purpose that allow:
    /// - number conversions (like <see cref="ToInt(string)"/>)
    /// - parsing (like <see cref="AfterFirst(string, string)"/>)
    /// - generation (like <see cref="Repeat(string, int)"/>)
    /// </summary>
    public static class StringExtensions
    {
        #region Unsigned Conversions

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="byte"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this string s)
        {
            byte.TryParse(s, out byte r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="ushort"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ushort ToUshort(this string s)
        {
            ushort.TryParse(s, out ushort r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="uint"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static uint ToUint(this string s)
        {
            uint.TryParse(s, out uint r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="ulong"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ulong ToUlong(this string s)
        {
            ulong.TryParse(s, out ulong r);
            return r;
        }

        #endregion

        #region Signed Conversions

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="sbyte"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static sbyte ToSbyte(this string s)
        {
            sbyte.TryParse(s, out sbyte r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="short"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short ToShort(this string s)
        {
            short.TryParse(s, out short r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="int"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            int.TryParse(s, out int r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="long"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToLong(this string s)
        {
            long.TryParse(s, out long r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="float"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToFloat(this string s)
        {
            float.TryParse(s, out float r);
            return r;
        }

        /// <summary>
        /// Converts a <see cref="string"/> to a <see cref="double"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double ToDouble(this string s)
        {
            double.TryParse(s, out double r);
            return r;
        }

        #endregion

        #region After & Before matches

        public static string AfterLast(this string str, string sub)
        {
            var idx = str.LastIndexOf(sub);
            return idx < 0 ? "" : str.Substring(idx + sub.Length);
        }

        public static string BeforeLast(this string str, string sub)
        {
            var idx = str.LastIndexOf(sub);
            return idx < 0 ? "" : str.Substring(0, idx);
        }

        public static string AfterFirst(this string str, string sub)
        {
            var idx = str.IndexOf(sub);
            return idx < 0 ? "" : str.Substring(idx + sub.Length);
        }

        public static string BeforeFirst(this string str, string sub)
        {
            var idx = str.IndexOf(sub);
            return idx < 0 ? "" : str.Substring(0, idx);
        }

        public static int PrefixMatch(this string str, string prefix)
        {
            int l = 0, slen = str.Length, plen = prefix.Length;
            while (l < slen && l < plen)
            {
                if (str[l] != prefix[l])
                    break;
                l++;
            }
            return l;
        }

        #endregion

        #region Repeat

        public static string Repeat(this string s, int number)
        {
            var builder = new StringBuilder("");
            for (int i = 0; i < number; ++i) builder.Append(s);
            return builder.ToString();
        }

        public static string Repeat(this string s, uint number)
        {
            var builder = new StringBuilder("");
            for (uint i = 0; i < number; ++i) builder.Append(s);
            return builder.ToString();
        }

        public static string Repeat(this string s, float number)
        {
            var builder = new StringBuilder("");
            for (int i = 0; i < number; ++i) builder.Append(s);
            return builder.ToString();
        }

        #endregion
    }
}
