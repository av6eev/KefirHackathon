using System;

namespace ServerCore.Main.Utilities.Event
{
    public class ReactiveEvent : IReactiveEvent
    {
        public event Action OnChanged;

        public void Invoke()
        {
            OnChanged?.Invoke();
        }
    }
    
    public class ReactiveEvent<T> : IReactiveEvent<T>
    {
        public event Action<T> OnChanged;

        public void Invoke(T element)
        {
            OnChanged?.Invoke(element);
        }
    }
}