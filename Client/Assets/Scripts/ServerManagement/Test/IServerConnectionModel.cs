using ServerCore.Main;

namespace ServerManagement.Test
{
    public interface IServerConnectionModel
    {
        Peer PlayerPeer { get; }
        Peer WorldPeer { get; }
        Host PlayerHost { get; }
        Host WorldHost { get; }
        void ConnectPlayer();
        void ConnectWorld();
        void DisconnectPlayer();
        void DisconnectWorld();
        void CompletePlayerConnect();
        void CompleteWorldConnect();
    }
}