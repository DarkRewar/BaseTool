using System;
using UnityEngine;

namespace BaseTool
{
    [Serializable]
    public class StringSavedVariable : SavedVariable<string>
    {
        public static implicit operator string(StringSavedVariable variable) => variable.Value;
        
        internal override void DeserializeFromFile(SaveFileSerializationContext context)
        {
            if(!context.Values.TryGetValue(Id, out object value))
                Debug.LogError($"No saved value found for {Id}.");
            else if(value is not string stringValue)
                Debug.LogError($"Variable {Id} is not a string.");
            else LoadValue(stringValue);
        }
    }
}