/**
 * Based on the plugin [Serializable-Dictionary-Unity](https://github.com/EduardMalkhasyan/Serializable-Dictionary-Unity)
 * Created by [EduardMalkhasyan](https://github.com/EduardMalkhasyan)
 */
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BaseTool
{
    [System.Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializedDictionaryKVPProps<TKey, TValue>> dictionaryList = new();

        public SerializableDictionary(Dictionary<TKey, TValue> reference) : base(reference)
        {
            BeforeSerialize();
        }

        private void BeforeSerialize()
        {
            foreach (var kVP in this)
            {
                if (dictionaryList.FirstOrDefault(value => this.Comparer.Equals(value.Key, kVP.Key))
                    is SerializedDictionaryKVPProps<TKey, TValue> serializedKVP)
                {
                    serializedKVP.Value = kVP.Value;
                }
                else
                {
                    dictionaryList.Add(kVP);
                }
            }

            dictionaryList.RemoveAll(value => ContainsKey(value.Key) == false);

            for (int i = 0; i < dictionaryList.Count; i++)
            {
                dictionaryList[i].index = i;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() => BeforeSerialize();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();

            dictionaryList.RemoveAll(r => r.Key == null);

            foreach (var serializedKVP in dictionaryList)
            {
                if (!(serializedKVP.isKeyDuplicated = ContainsKey(serializedKVP.Key)))
                {
                    Add(serializedKVP.Key, serializedKVP.Value);
                }
            }
        }

        public new TValue this[TKey key]
        {
            get
            {
#if UNITY_EDITOR
                if (ContainsKey(key))
                {
                    var duplicateKeysWithCount = dictionaryList.GroupBy(item => item.Key)
                                                               .Where(group => group.Count() > 1)
                                                               .Select(group => new { Key = group.Key, Count = group.Count() });

                    foreach (var duplicatedKey in duplicateKeysWithCount)
                    {
                        Debug.LogError($"Key '{duplicatedKey.Key}' is duplicated {duplicatedKey.Count} times in the dictionary.");
                    }

                    return base[key];
                }
                else
                {
                    Debug.LogError($"Key '{key}' not found in dictionary.");
                    return default(TValue);
                }
#else
                return base[key];
#endif
            }
        }

        [System.Serializable]
        public class SerializedDictionaryKVPProps<TypeKey, TypeValue>
        {
            public TypeKey Key;
            public TypeValue Value;

            public int index;
            public bool isKeyDuplicated;

            public SerializedDictionaryKVPProps(TypeKey key, TypeValue value) { this.Key = key; this.Value = value; }

            public static implicit operator SerializedDictionaryKVPProps<TypeKey, TypeValue>(KeyValuePair<TypeKey, TypeValue> kvp)
                => new SerializedDictionaryKVPProps<TypeKey, TypeValue>(kvp.Key, kvp.Value);
            public static implicit operator KeyValuePair<TypeKey, TypeValue>(SerializedDictionaryKVPProps<TypeKey, TypeValue> kvp)
                => new KeyValuePair<TypeKey, TypeValue>(kvp.Key, kvp.Value);
        }
    }
}