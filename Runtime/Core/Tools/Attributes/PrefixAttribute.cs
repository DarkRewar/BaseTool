using UnityEngine;

namespace BaseTool
{
    public class PrefixAttribute : PropertyAttribute
    {
        public string Text { get; protected set; }

        public PrefixAttribute(string text)
        {
            Text = text;
        }
    }
}
