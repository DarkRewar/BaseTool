using UnityEngine;

namespace BaseTool.Tools.Attributes
{
    public class MinMaxAttribute : PropertyAttribute
    {
        public float MinLimit { get; } = 0;
        public float MaxLimit { get; } = 1;

        public MinMaxAttribute(int min, int max)
        {
            MinLimit = min;
            MaxLimit = max;
        }

        public MinMaxAttribute(float min, float max)
        {
            MinLimit = min;
            MaxLimit = max;
        }
    }
}
