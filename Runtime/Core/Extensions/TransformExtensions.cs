using UnityEngine;

namespace BaseTool
{
    public static class TransformExtensions
    {
        /// <summary>
        /// <see cref="Object.Destroy(Object)"/> every direct child of the transform.
        /// </summary>
        /// <param name="transform"></param>
        public static void Clear(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// <see cref="Object.Destroy(Object)"/> every direct child of the transform.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="time"></param>
        public static void Clear(this Transform transform, float time)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject, time);
            }
        }

        /// <summary>
        /// <see cref="Object.DestroyImmediate(Object)"/> every direct child of the transform.
        /// </summary>
        /// <param name="transform"></param>
        public static void ClearImmediate(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
