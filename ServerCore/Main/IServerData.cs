using System.Collections.Generic;
using ServerCore.Main.Property;

namespace ServerCore.Main
{
    public interface IServerData
    {
        string Id { get; }
        bool IsDirty { get; }
        bool IsNew { get; }
        Dictionary<string, object> Read(Protocol protocol);
        void WriteAll(Protocol protocol);
        bool Write(Protocol protocol);
        bool HasChanges();
        void ChangeIsNew(bool state);
    }
}