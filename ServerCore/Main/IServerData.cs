using System.Collections.Generic;
using ServerCore.Main.Property;

namespace ServerCore.Main
{
    public interface IServerData
    {
        string Id { get; }
        bool IsDirty { get; }
        void Read(Protocol protocol);
        bool Write(Protocol protocol);
        bool HasChanges();
    }
}