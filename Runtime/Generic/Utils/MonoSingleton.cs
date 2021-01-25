using UnityEngine;

namespace BaseTool.Generic.Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;
        
        [Header("Singleton Settings")]
        public bool DestroyIfAlreadyExists = false;

        protected virtual void Awake()
        {
            if (DestroyIfAlreadyExists) DestroyImmediate(gameObject);
            Instance = this as T;
        }
    }
}