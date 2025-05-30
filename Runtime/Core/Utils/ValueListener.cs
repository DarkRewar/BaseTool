using System;
using UnityEngine;

namespace BaseTool
{
    /// <summary>
    /// Observer pattern to handle value change events.
    /// It is recommended to declare your value as a readonly 
    /// property/field and only modify the <see cref="Value"/>
    /// to trigger the <see cref="OnChanged"/> event.
    /// </summary>
    /// <typeparam name="T"></typeparam
    [Serializable]
    public sealed class ValueListener<T>
    {
        [SerializeField]
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                if (CheckValue && _value is not null && value is not null && _value.Equals(value)) return;
                var oldValue = _value;
                _value = value;
                OnChanged?.Invoke(oldValue, _value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether value changes should
        /// be validated before triggering the <see cref="OnChanged"/> event.
        /// When set to <c>true</c>, the <see cref="Value"/> will not trigger
        /// the <see cref="OnChanged"/> event for assignments where the new value
        /// is equal to the current value, based on <see cref="object.Equals"/>.
        /// Set to <c>false</c> to disable this behavior and always trigger the event
        /// regardless of value similarity.
        /// </summary>
        public bool CheckValue { get; set; } = true;

        public event ValueChangedEventHandler OnChanged;
        public delegate void ValueChangedEventHandler(T oldValue, T newValue);

        public ValueListener() => _value = default;

        public ValueListener(T value) => _value = value;

        public static implicit operator ValueListener<T>(T value) => new(value);
        public static implicit operator T(ValueListener<T> listener) => listener.Value;
    }
}
