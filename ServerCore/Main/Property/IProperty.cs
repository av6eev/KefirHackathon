using System;

namespace ServerCore.Main.Property
{
    public interface IProperty
    {
        event Action Changed;
        string Id { get; }
        bool IsChanged { get; }
        void SetFromProtocol(Protocol protocol, out object value);
        void GetForProtocol(Protocol protocol);
    }
}