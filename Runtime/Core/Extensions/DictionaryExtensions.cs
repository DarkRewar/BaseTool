using System.Collections.Generic;

namespace BaseTool
{
    public static class DictionaryExtensions
    {
        public static SerializableDictionary<TKey, TValue> ToSerializableDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return new SerializableDictionary<TKey, TValue>(dictionary);
        }
    }
}
