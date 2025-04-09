using UnityEngine;

namespace BaseTool
{
    public class FloatSavedVariable : SavedVariable<float>
    {
        public static implicit operator float(FloatSavedVariable variable) => variable.Value;
        
        internal override void DeserializeFromFile(SaveFileSerializationContext context)
        {
            if(!context.Values.TryGetValue(Id, out object value))
                Debug.LogError($"No saved value found for {Id}.");
            else if(value is not double doubleValue)
                Debug.LogError($"Variable {Id} is not an number.");
            else
            {
                LoadValue((float) doubleValue);
            }
        }
    }
}