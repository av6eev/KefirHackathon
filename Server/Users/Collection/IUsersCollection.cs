using Server.Utilities.ModelCollection;
using ServerCore.Main;

namespace Server.Users.Collection
{
    public interface IUsersCollection : IModelCollection<string, UserModel>
    {
        UserModel Add(Peer peer, string id, string nickname);
        void Add(UserModel userData);
        void Remove(UserModel userData);
        void TimeoutUser(Peer peer);
        void DisconnectUser(Peer netEventPeer);
        bool TryGetUser(string id, out UserModel userData);
        bool TryGetUser(Peer peer, out UserModel userData);
        bool TryGetUserByNickname(string nickname, out UserModel userData);
    }
}