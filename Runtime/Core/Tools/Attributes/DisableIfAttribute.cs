using System;

namespace BaseTool
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DisableIfAttribute : ConditionalAttribute
    {
        public DisableIfAttribute(string selector) : base(selector) { }
    }
}
