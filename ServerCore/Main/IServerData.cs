using System.Collections.Generic;
using ServerCore.Main.Property;

namespace ServerCore.Main
{
    public interface IServerData
    {
        Dictionary<string, IProperty> Properties { get; }
        string Id { get; }
        void Read(Protocol protocol);
        bool Write(Protocol protocol);
    }
}