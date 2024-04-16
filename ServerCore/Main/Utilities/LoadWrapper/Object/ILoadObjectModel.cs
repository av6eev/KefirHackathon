using System;
using ServerCore.Main.Utilities.Awaiter;

namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public interface ILoadObjectModel<T> : ILoadElement, IDisposable
    {
        CustomAwaiter LoadAwaiter { get; }
        CustomAwaiter UnloadAwaiter { get; }
        T Result { get; }
    }
}