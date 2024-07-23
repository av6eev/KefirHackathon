using Server.Users.Friends.Invite;
using Server.Utilities.ModelCollection;
using ServerCore.Main.Utilities.Event;

namespace Server.Users.Friends;

public class UserFriendsCollection : ModelCollection<string>
{
    public readonly ReactiveEvent NotifySaveEvent;
    
    public readonly ModelCollection<string, UserFriendInviteModel> Invites = new();
    public readonly ModelCollection<string> OnlineFriends = new();

    public UserFriendsCollection(ReactiveEvent notifySaveEvent)
    {
        NotifySaveEvent = notifySaveEvent;
    }

    public void AddFriend(string nickname)
    {
        OnlineFriends.Add(nickname);
        Add(nickname);
    }
    
    public void RemoveFriend(string nickname, bool isFriendOnline = true)
    {
        if (isFriendOnline)
        {
            OnlineFriends.Remove(nickname);
        }
        
        Remove(nickname);
    }
    
    public UserFriendInviteModel CreateFriendInvite(string fromUserId, string fromUserNickname, string invitedUserId)
    {
        var inviteModel = new UserFriendInviteModel(fromUserId, fromUserNickname, invitedUserId);
        
        Invites.Add(inviteModel.InviteId, inviteModel);

        return inviteModel;
    }
    
    public void RemoveInvite(string inviteId)
    {
        if (!Invites.TryGetModel(inviteId, out var inviteModel)) return;

        Invites.Remove(inviteModel.InviteFromUserId);
    }

    public bool TryGetInvite(string inviteId, out UserFriendInviteModel inviteModel)
    {
        if (Invites.TryGetModel(inviteId, out var invite))
        {
            inviteModel = invite;
            return true;
        }

        inviteModel = null;
        return false;
    }
}