using System.Collections.Generic;
using UnityEngine;

namespace BaseTool
{
    /// <summary>
    /// System that manages every <see cref="Cooldown"/> that wants
    /// to be automatically updated. <br/>
    /// It is for BaseTool internal usage.
    /// </summary>
    internal class CooldownManager : MonoSingleton<CooldownManager>
    {
        private static List<Cooldown> _cooldowns = new List<Cooldown>();

        protected override void Awake()
        {
            _dontDestroyOnLoad = true;
            base.Awake();
        }

        internal void OnDestroy() => _cooldowns.Clear();

        internal void Update()
        {
            for (int i = _cooldowns.Count - 1; i >= 0; --i)
                _cooldowns[i].Update();
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Init() => GetOrCreateInstance();

        internal static void Subscribe(Cooldown c) => _cooldowns.Add(c);

        internal static void Unsubscribe(Cooldown c) => _cooldowns.Remove(c);
    }
}