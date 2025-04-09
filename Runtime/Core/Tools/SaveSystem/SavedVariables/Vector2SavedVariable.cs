using System;
using UnityEngine;

namespace BaseTool
{
    public class Vector2SavedVariable : SavedVariable<Vector2>
    {
        public static implicit operator Vector2(Vector2SavedVariable variable) => variable.Value;
        
        internal override void DeserializeFromFile(SaveFileSerializationContext context)
        {
            if(!context.Values.TryGetValue(Id, out object value))
                Debug.LogError($"No saved value found for {Id}.");
            else if(value is not string vectorString)
                Debug.LogError($"Variable {Id} could not be deserialized.");
            else
            {
                string[] split = vectorString.Split(',');
                if (split.Length != 2) throw new FormatException($"Variable {Id} is not a valid Vector2.");
                Vector2 vector2 = new Vector2(float.Parse(split[0]), float.Parse(split[1]));
                LoadValue(vector2);
            }
        }

        internal override void SerializeToFile(SaveFileSerializationContext context)
        {
            context.Values[Id] = $"{Value.x},{Value.y}";
        }
    }
}