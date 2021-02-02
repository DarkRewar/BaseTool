namespace BaseTool.Generic.Utils
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
    }
}
