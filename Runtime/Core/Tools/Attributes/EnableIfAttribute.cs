using System;

namespace BaseTool
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnableIfAttribute : ConditionalAttribute
    {
        public EnableIfAttribute(string selector) : base(selector) { }
    }
}
