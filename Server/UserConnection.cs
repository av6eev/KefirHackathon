using ServerCore.Main;

namespace Server;

public class UserConnection
{
    public string PlayerId;
    public Peer Peer;
    public bool FirstConnection = true;

    public UserConnection(Peer peer, string playerId)
    {
        Peer = peer;
        PlayerId = playerId;
    }
}