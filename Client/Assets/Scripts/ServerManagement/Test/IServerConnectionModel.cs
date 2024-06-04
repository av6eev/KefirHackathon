using ServerCore.Main;

namespace ServerManagement.Test
{
    public interface IServerConnectionModel
    {
        Peer PlayerPeer { get; }
        Host PlayerHost { get; }
        void ConnectPlayer();
        void DisconnectPlayer();
        void CompletePlayerConnect();
    }
}