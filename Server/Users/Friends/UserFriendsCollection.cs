using Server.Users.Friends.Invite;
using Server.Utilities.ModelCollection;
using ServerCore.Main.Utilities.Event;

namespace Server.Users.Friends;

public class UserFriendsCollection : ModelCollection<string>
{
    public readonly ReactiveEvent NotifySaveEvent;
    
    public event Action<UserFriendInviteModel> OnInviteCreated;
    public event Action<UserFriendInviteModel> OnInviteRemoved;
    
    public readonly Dictionary<string, UserFriendInviteModel> Invites = new();

    public UserFriendsCollection(ReactiveEvent notifySaveEvent)
    {
        NotifySaveEvent = notifySaveEvent;
    }

    public UserFriendInviteModel CreateFriendInvite(string fromUserId, string fromUserNickname, string invitedUserId)
    {
        var inviteModel = new UserFriendInviteModel(fromUserId, fromUserNickname, invitedUserId);
        
        Invites.Add(inviteModel.InviteId, inviteModel);
        
        OnInviteCreated?.Invoke(inviteModel);

        return inviteModel;
    }
    
    public void RemoveInvite(string inviteId)
    {
        if (!Invites.TryGetValue(inviteId, out var inviteModel)) return;

        Invites.Remove(inviteModel.InviteFromUserId);
        
        OnInviteRemoved?.Invoke(inviteModel);
    }

    public bool TryGetInvite(string inviteId, out UserFriendInviteModel inviteModel)
    {
        if (Invites.TryGetValue(inviteId, out var invite))
        {
            inviteModel = invite;
            return true;
        }

        inviteModel = null;
        return false;
    }
}