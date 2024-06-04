using System;
using ServerCore.Main.Utilities.Awaiter;

namespace ServerCore.Main.Utilities.LoadWrapper.Object
{
    public interface ILoadObjectModel : ILoadElement, IDisposable
    {
        CustomAwaiter LoadAwaiter { get; }
        CustomAwaiter UnloadAwaiter { get; }
        string Result { get; }
        CustomAwaiter Load();
    }
}