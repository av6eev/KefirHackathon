using System.Collections;
using System.Collections.Generic;

namespace ServerCore.Main.Users.Collection
{
    public interface IUsersCollection
    {
        void Add(Peer peer, string userId);
        void Add(UserData userData);
        void Remove(UserData userData);
        bool TryGetUser(Peer peer, out UserData userData);
        bool TryGetUser(string id, out UserData userData);
        bool TryGetUserByNickname(string nickname, out UserData userData);
        IEnumerable<UserData> GetUsers();
    }
}