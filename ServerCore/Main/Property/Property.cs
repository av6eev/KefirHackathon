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
                if (value != null && value.Equals(_value))
                {
                    return;
                }
                
                _value = value;
                
                if (NotifyChanged)
                {
                    IsChanged = true;
                    Changed?.Invoke();    
                }
            }
            get => _value;
        }

        public string Id { get; }

        public bool IsChanged { get; set; }
        private bool NotifyChanged { get; } 

        public Property(string key, T value, bool notifyChanged = true)
        {
            Id = key;
            Value = value;
            NotifyChanged = notifyChanged;
        }

        public void SetFromProtocol(Protocol protocol, out object value)
        {
            protocol.Get(out _value);
            Changed?.Invoke();
            value = _value;
        }

        public void GetForProtocol(Protocol protocol)
        {
            protocol.Add(_value);
            IsChanged = false;
        }
    }
}