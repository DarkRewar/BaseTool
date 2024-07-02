using System;
using UnityEngine;

namespace BaseTool
{
    /// <summary>
    /// System to easily use cooldown as float values.<br/>
    /// Example : public Cooldown myCooldown = 5f;<br/>
    /// Then, you must update the cooldown with : myCooldown.Update(Time.deltaTime);
    /// </summary>
    [Serializable]
    public class Cooldown
    {
        /// <summary>
        /// The cooldown duration, in seconds.
        /// </summary>
        [Min(0), Tooltip("The cooldown duration, in seconds.")]
        public float Duration = 1;

        /// <summary>
        /// The time left before the cooldown is ready.
        /// </summary>
        public float TimeLeft { get; private set; } = 0;

        /// <summary>
        /// The time left before the cooldown is ready, in percent, between 0 and 1.
        /// </summary>
        public float TimeLeftPercent => TimeLeft / Duration;

        public bool IsReady => TimeLeft <= 0;

        /// <summary>
        /// Determines if the cooldown must <see cref="Update"/> or not.<br/>
        /// If true, the <see cref="Update"/> will be ignored.
        /// </summary>
        public bool IsPaused { get; private set; } = false;

        /// <summary>
        /// If true, let the <see cref="Cooldown"/> be managed and updated by
        /// the <see cref="CooldownManager"/>.<br/>
        /// If you want to update the cooldown on your own, set this to false.<br/>
        /// True by default.
        /// </summary>
        public bool SubscribeToManager = true;

        /// <summary>
        /// Event triggered once the cooldown is ready.
        /// </summary>
        public event Action OnReady;

        public Cooldown(float value) => Duration = value;

        /// <summary>
        /// Update the cooldown time left. By default, with the <see cref="Time.deltaTime"/> value.
        /// </summary>
        public void Update() => Update(Time.deltaTime);

        /// <summary>
        /// Update the cooldown time left with the elapsed time passed by parameter.
        /// </summary>
        /// <param name="time">Elapsed time, in seconds</param>
        public void Update(float time)
        {
            if (IsReady || IsPaused) return;
            TimeLeft -= time;

            if (IsReady)
            {
                if (SubscribeToManager)
                    CooldownManager.Unsubscribe(this);
                OnReady?.Invoke();
            }
        }

        /// <summary>
        /// Reset the cooldown timer.
        /// </summary>
        public void Reset()
        {
            if (SubscribeToManager)
                CooldownManager.Subscribe(this);
            TimeLeft = Duration;
        }

        /// <summary>
        /// Reset the cooldown if it's ready.
        /// Return true if the cooldown has been reset,
        /// else false.
        /// </summary>
        /// <returns></returns>
        public bool Restart()
        {
            if (!IsReady) return false;

            Reset();
            return true;
        }

        /// <summary>
        /// Pauses the cooldown. <see cref="Update"/> will totally be ignored
        /// until the <see cref="Resume"/> is called or the <see cref="IsPaused"/>
        /// is set to false.
        /// </summary>
        public void Pause() => IsPaused = !IsPaused && !IsReady;

        /// <summary>
        /// Unpauses the cooldown by setting <see cref="IsPaused"/> to false.
        /// </summary>
        public void Resume() => IsPaused = false;

        /// <summary>
        /// Totally stops the cooldown (also removes it from the <see cref="CooldownManager"/>)
        /// and set the time left to 0. The cooldown becomes ready.
        /// </summary>
        public void Stop()
        {
            if (IsReady) return;

            if (SubscribeToManager)
                CooldownManager.Unsubscribe(this);
            TimeLeft = 0;
            IsPaused = false;
        }

        /// <summary>
        /// Cast implicitly a float into a cooldown. Really useful to
        /// construct a cooldown without writing "new" keyword.
        /// Example: Cooldown cd = 1;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator Cooldown(float value) => new Cooldown(value);
    }
}
