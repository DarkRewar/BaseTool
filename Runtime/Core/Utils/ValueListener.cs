namespace BaseTool
{
    /// <summary>
    /// Observer pattern to handle value change events.
    /// It is recommended to declare your value as a readonly 
    /// property/field and only modify the <see cref="Value"/>
    /// to trigger the <see cref="OnChanged"/> event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ValueListener<T>
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                var oldValue = _value;
                _value = value;
                OnChanged?.Invoke(oldValue, _value);
            }
        }

        public event ValueChangedEventHandler OnChanged;
        public delegate void ValueChangedEventHandler(T oldValue, T newValue);

        public ValueListener() => _value = default;

        public ValueListener(T value) => _value = value;

        public static implicit operator ValueListener<T>(T value) => new(value);
        public static implicit operator T(ValueListener<T> listener) => listener.Value;
    }
}
