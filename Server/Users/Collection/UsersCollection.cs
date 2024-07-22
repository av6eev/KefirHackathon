using Server.Utilities.ModelCollection;
using ServerCore.Main;
using ServerCore.Main.Utilities.Logger;

namespace Server.Users.Collection
{
    public class UsersCollection : ModelCollection<string, UserModel>, IUsersCollection
    {
        public const string SaveId = "users";
        
        public UserModel Add(Peer peer, string id, string nickname)
        {
            var userModel = new UserModel(peer, id, nickname);
        
            Add(userModel.PlayerId, userModel);

            return userModel;
        }

        public void Add(UserModel user)
        {
            Add(user.PlayerId, user);
        }

        public void Remove(UserModel user)
        {
            Remove(user.PlayerId);
        }

        public void TimeoutUser(Peer peer)
        {
            if (!TryGetUser(peer, out var user))
            {
                Logger.Instance.Log($"Cannot find and timeout user with peer id: {peer.ID}");
                return;
            }
            
            Remove(user);
        }

        public void DisconnectUser(Peer peer)
        {
            if (TryGetUser(peer, out var userModel))
            {
                Remove(userModel);
            }
        }

        public bool TryGetUser(string id, out UserModel userData)
        {
            if (Collection.TryGetValue(id, out var user))
            {
                userData = user;
                return true;
            }

            userData = null;
            return false;
        }

        public bool TryGetUser(Peer peer, out UserModel user)
        {
            var users = Collection.Values.Where(user => user.Peer.ID == peer.ID).ToList();

            if (users.Any())
            {
                user = users.First();
                return true;
            }

            user = null;
            return false;
        }

        public bool TryGetUserByNickname(string nickname, out UserModel user)
        {
            var users = Collection.Values.Where(user => user.PlayerNickname == nickname).ToList();
            
            if (users.Any())
            {
                user = users.First();
                return true;
            }

            user = null;
            return false;
        }
    }
}