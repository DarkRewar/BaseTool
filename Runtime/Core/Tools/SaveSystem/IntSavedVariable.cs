using UnityEngine;

namespace BaseTool
{
    public class IntSavedVariable : SavedVariable<int>
    {
        public static implicit operator int(IntSavedVariable variable) => variable.Value;
        
        internal override void DeserializeFromFile(SaveFileSerializationContext context)
        {
            if(!context.Values.TryGetValue(Id, out object value))
                Debug.LogError($"No saved value found for {Id}.");
            else if(value is not long longValue)
                Debug.LogError($"Variable {Id} is not an number.");
            else
            {
                LoadValue((int) longValue);
            }
        }

        internal override void SerializeToFile(SaveFileSerializationContext context)
        {
            context.Values[Id] = Value;
        }
    }
}