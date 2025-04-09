using System;
using UnityEngine;

namespace BaseTool
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredAttribute : PropertyAttribute
    {
        public string Message { get; }
        
        public RequiredAttribute() {}
        
        public RequiredAttribute(string message) => Message = message;
    }
}