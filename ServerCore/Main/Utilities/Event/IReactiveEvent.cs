using System;

namespace ServerCore.Main.Utilities.Event
{
    public interface IReactiveEvent
    {
        event Action OnChanged;
    }
    
    public interface IReactiveEvent<out T>
    {
        event Action<T> OnChanged;
    }
}