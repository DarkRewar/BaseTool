using System;
using UnityEngine;

namespace BaseTool
{
    [Serializable]
    public class StringSavedVariable : SavedVariable
    {
        public string Value;
        
        public static implicit operator string(StringSavedVariable variable) => variable.Value;
        
        internal override object GetSavedValue() => Value;
        
        internal override void SetSavedValue(object value)
        {
            if(value is string stringValue) Value = stringValue;
            else Debug.LogError($"{_id} can't be converted to string");
        }
    }
}