using System;

namespace ServerCore.Main.Property
{
    public interface IProperty
    {
        event Action Changed;
        string Id { get; }
        bool IsChanged { get; }
        void SetFromProtocol(Protocol protocol);
        void GetForProtocol(Protocol protocol);
    }
}