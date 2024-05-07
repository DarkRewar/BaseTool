using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaseTool
{
    public interface ICustomTick
    {
        public bool ShouldTick(ulong tick);
    }

    [AddComponentMenu("BaseTool/Core/Tick Manager")]
    [HelpURL("https://github.com/DarkRewar/BaseTool?tab=readme-ov-file#tickmanager")]
    public class TickManager : MonoBehaviour
    {
        #region SINGLETON

        private static TickManager _instance;
        public static TickManager Instance
        {
            get
            {
                if (_instance) return _instance;
                throw new NotASingletonException($"If you want to use {nameof(TickManager)} as singleton, make sur to check `Make Singleton` field.");
            }
        }

        private void Awake()
        {
            if (_makeSingleton)
                _instance = this;
        }

        #endregion

        [SerializeField] private bool _makeSingleton = false;
        [SerializeField] private bool _tickOnUnscaledDeltaTime = false;
        [SerializeField, Min(0)] private float _tickDuration = 1;

        private float _timeLeft = 0;
        private ulong _currentTick = 0;

        private readonly Dictionary<ushort, (ICustomTick tick, Action delegates)> _customTicks =
            new Dictionary<ushort, (ICustomTick, Action)>();

        public bool IsPaused = false;

        public event Action OnTick;
        public UnityEvent OnTickEvent;

        void Update()
        {
            if (IsPaused) return;

            if (_timeLeft <= 0)
            {
                _currentTick++;
                _timeLeft += _tickDuration;

                OnTick?.Invoke();
                OnTickEvent?.Invoke();
                ProcessCustomTicks();
            }
            else
            {
                _timeLeft -= _tickOnUnscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;
            }
        }

        private void ProcessCustomTicks()
        {
            foreach (var customTicks in _customTicks.Values)
            {
                if (customTicks.tick.ShouldTick(_currentTick))
                    customTicks.delegates?.Invoke();
            }
        }

        private static ushort GetTypeId<T>() => (ushort)(typeof(T).FullName.GetHashCode() >> 16);

        /// <summary>
        /// Add an additionnal custom tick callback to the <see cref="TickManager"/>.<br/>
        /// The type of tick must inherits from <see cref="ICustomTick"/> interface.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public void RegisterCustomTick<T>(Action callback) where T : ICustomTick, new()
        {
            var key = GetTypeId<T>();
            if (_customTicks.TryGetValue(key, out var pair))
            {
                pair.delegates += callback;
                _customTicks[key] = pair;
            }
            else _customTicks.Add(key, (new T(), callback));
        }

        /// <summary>
        /// Remove a specific custom tick callback from the <see cref="TickManager"/>.<br/>
        /// The type of tick must inherits from <see cref="ICustomTick"/> interface.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public void UnregisterCustomTick<T>(Action callback) where T : ICustomTick
        {
            var key = GetTypeId<T>();
            if (_customTicks.TryGetValue(key, out var pair))
            {
                pair.delegates -= callback;
                _customTicks[key] = pair;
            }
        }

        /// <summary>
        /// Remove every custom tick callback from the <see cref="TickManager"/>.<br/>
        /// The type of tick must inherits from <see cref="ICustomTick"/> interface.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        public void UnregisterCustomTicks<T>() where T : ICustomTick
        {
            var key = GetTypeId<T>();
            if (_customTicks.ContainsKey(key))
                _customTicks.Remove(key);
        }

        /// <summary>
        /// Remove every custom tick callbacks of any type from the <see cref="TickManager"/>.
        /// </summary>
        public void UnregisterCustomTicks() => _customTicks.Clear();
    }
}
