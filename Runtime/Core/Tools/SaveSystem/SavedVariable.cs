using System;
using UnityEngine;

namespace BaseTool
{
    public abstract class SavedVariable : ScriptableObject
    {
        [Header("Saved Variable Description")] [SerializeField]
        protected string _id = "SavedVariable";
        
        public string Id => _id;

        [SerializeField] [TextArea(3, 10)] protected string _description;
        
        internal abstract object GetSavedValue();
        
        internal abstract void SetSavedValue(object value);

        private void OnValidate()
        {
            name = _id;
        }
    }
}