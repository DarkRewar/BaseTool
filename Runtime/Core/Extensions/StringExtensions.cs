using System.Text;

namespace BaseTool
{
    /// <summary>
    /// StringUtils permet d'ajouter des extensions de méthodes
    /// pour les objets string.
    /// </summary>
    public static class StringExtensions
    {
        #region Unsigned Conversions

        /// <summary>
        /// Conversion d'un string en byte (UInt8).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this string s)
        {
            byte.TryParse(s, out byte r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en UInt16.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static ushort ToUshort(this string s)
        {
            ushort.TryParse(s, out ushort r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en UInt32.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static uint ToUint(this string s)
        {
            uint.TryParse(s, out uint r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en UInt64.
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
        /// Conversion d'un string en byte (Int8).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static sbyte ToSbyte(this string s)
        {
            sbyte.TryParse(s, out sbyte r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en Int16.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static short ToShort(this string s)
        {
            short.TryParse(s, out short r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en Int32.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            int.TryParse(s, out int r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en Int64.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToLong(this string s)
        {
            long.TryParse(s, out long r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en float.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ToFloat(this string s)
        {
            float.TryParse(s, out float r);
            return r;
        }

        /// <summary>
        /// Conversion d'un string en double.
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
            for (int i = 0; i < number; ++i) builder.Append(s);
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
