using System;
using UnityEngine;

namespace BaseTool
{
    [AttributeUsage(AttributeTargets.Field)]
    public class IfAttribute : ConditionalAttribute
    {
        public IfAttribute(string selector) : base(selector) { }
    }
}