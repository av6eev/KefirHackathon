using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ServerCore.Main.Property;

namespace ServerCore.Main.Users.Collection
{
    public class UsersCollection : IUsersCollection
    {
        public readonly Dictionary<Peer, UserData> UsersByPeer = new();
        public readonly Dictionary<string, UserData> UsersById = new();

        public void Add(Peer peer, string userId)
        {
            var userData = new UserData
            {
                PlayerId = { Value = userId },
                Peer = peer,
                WorldFirstConnection = true
            };
        
            UsersByPeer.Add(peer, userData);
            UsersById.Add(userId, userData);
        }

        public void Add(UserData userData)
        {
            UsersByPeer.Add(userData.Peer, userData);
            UsersById.Add(userData.PlayerId.Value, userData);
        }
    
        public void Remove(UserData userData)
        {
            UsersByPeer.Remove(userData.Peer);
            UsersById.Remove(userData.PlayerId.Value);
        }
        
        public bool TryGetUser(Peer peer, out UserData userData)
        {
            if (UsersByPeer.TryGetValue(peer, out var data))
            {
                userData = data;
                return true;
            }

            userData = null;
            return false;
        }

        public bool TryGetUser(string id, out UserData userData)
        {
            if (UsersById.TryGetValue(id, out var user))
            {
                userData = user;
                return true;
            }

            userData = null;
            return false;
        }

        public IEnumerable<UserData> GetUsers()
        {
            foreach (var user in UsersByPeer)
            {
                yield return user.Value;
            }
        }
    }
}