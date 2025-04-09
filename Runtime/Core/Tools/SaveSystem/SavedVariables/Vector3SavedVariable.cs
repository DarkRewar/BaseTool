using System;
using UnityEngine;

namespace BaseTool
{
    public class Vector3SavedVariable : SavedVariable<Vector3>
    {
        public static implicit operator Vector3(Vector3SavedVariable variable) => variable.Value;
        
        internal override void DeserializeFromFile(SaveFileSerializationContext context)
        {
            if(!context.Values.TryGetValue(Id, out object value))
                Debug.LogError($"No saved value found for {Id}.");
            else if(value is not string vectorString)
                Debug.LogError($"Variable {Id} could not be deserialized.");
            else
            {
                string[] split = vectorString.Split(',');
                if (split.Length != 3) throw new FormatException($"Variable {Id} is not a valid Vector3.");
                Vector3 vector3 = new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));
                LoadValue(vector3);
            }
        }

        internal override void SerializeToFile(SaveFileSerializationContext context)
        {
            context.Values[Id] = $"{Value.x},{Value.y},{Value.z}";
        }
    }
}