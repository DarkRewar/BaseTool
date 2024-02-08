using System;

namespace BaseTool
{
    [AttributeUsage(AttributeTargets.Field)]
    public class IfNotAttribute : ConditionalAttribute
    {
        public IfNotAttribute(string selector) : base(selector) { }
    }
}