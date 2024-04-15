using System;

namespace ServerCore.Main.Property
{
    public class Property<T> : IProperty
    {
        public event Action Changed;

        private T _value;
        public T Value
        {
            set
            {
                _value = value;
                IsChanged = true;
                Changed?.Invoke();
            }
            get => _value;
        }

        public string Id { get; }

        public bool IsChanged { get; set; }

        public Property(string key, T value)
        {
            Id = key;
            Value = value;
        }

        public void SetFromProtocol(Protocol protocol)
        {
            protocol.Get(out _value);
            Changed?.Invoke();
        }

        public void GetForProtocol(Protocol protocol)
        {
            protocol.Add(_value);
            IsChanged = false;
        }
    }
}