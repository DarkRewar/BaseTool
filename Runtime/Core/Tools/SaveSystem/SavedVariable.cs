using System;
using UnityEngine;

namespace BaseTool
{
    [Serializable]
    public abstract class SavedVariable : ScriptableObject
    {
        [Header("Saved Variable Description")] [SerializeField]
        protected string _id = "SavedVariable";
        
        public string Id => _id;

        [SerializeField] [TextArea(3, 10)] protected string _description;

        internal abstract void DeserializeFromFile(SaveFileSerializationContext context);
        
        internal abstract void SerializeToFile(SaveFileSerializationContext context);

        private void OnValidate()
        {
            name = _id;
        }
    }

    public abstract class SavedVariable<T> : SavedVariable
    {
        public T Value;
        
        public event Action<T> OnValueLoaded;

        internal override void DeserializeFromFile(SaveFileSerializationContext context) => throw new NotImplementedException();

        internal override void SerializeToFile(SaveFileSerializationContext context)
        {
            context.Values[Id] = Value;
        }

        protected void LoadValue(T value)
        {
            Value = value;
            OnValueLoaded?.Invoke(Value);
        }
    }
}