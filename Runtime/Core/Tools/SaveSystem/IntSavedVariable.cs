using UnityEngine;

namespace BaseTool
{
    public class IntSavedVariable : SavedVariable
    {
        public int Value;
        
        public static implicit operator int(IntSavedVariable variable) => variable.Value;
        
        internal override object GetSavedValue() => Value;
        
        internal override void SetSavedValue(object value)
        {
            Value = (int)value;
            // else Debug.LogError($"{_id} can't be converted to int");
        }
    }
}