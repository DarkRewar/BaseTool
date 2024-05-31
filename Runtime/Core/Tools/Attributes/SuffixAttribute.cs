using UnityEngine;

namespace BaseTool
{
    public class SuffixAttribute : PropertyAttribute
    {
        public string Text { get; protected set; }

        public SuffixAttribute(string text)
        {
            Text = text;
        }
    }
}
