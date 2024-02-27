using UnityEngine;

namespace BaseTool
{
    /// <summary>
    /// Manage a MonoBehaviour to be a singleton.
    /// Consider to use the <see cref="Instance"/> accessor to
    /// retrieve the current instance, but prefer to use
    /// <see cref="GetOrCreateInstance(bool)"/> which automatically
    /// creates the instance if it doesn't exist.
    /// </summary>
    /// <typeparam name="T">Type of the singleton class</typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// The current instance of the singleton.
        /// </summary>
        public static T Instance { get; protected set; }

        [Header("Singleton Settings"), SerializeField]
        protected bool _dontDestroyOnLoad = false;

        protected virtual void Awake()
        {
            if (Instance != this && Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            else if (Instance == null)
            {
                Instance = this as T;
            }

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Try to get the singleton instance. If don't exists,
        /// create one and returns it.
        /// </summary>
        /// <param name="dontDestroy"></param>
        /// <returns></returns>
        public static T GetOrCreateInstance(bool dontDestroy = false)
        {
            if (!Instance)
            {
                Instance = FindAnyObjectByType<T>();
            }

            if (!Instance)
            {
                GameObject instanceObj = new GameObject($"{typeof(T).Name} (singleton)");
                Instance = instanceObj.AddComponent<T>();
                if (dontDestroy)
                    DontDestroyOnLoad(instanceObj);
            }

            return Instance;
        }
    }
}