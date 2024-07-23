using Server.Utilities.Model;

namespace Server.Users.Friends.Invite;

public class UserFriendInviteModel : IModel
{
    public event Action<bool> OnDecided;

    public readonly string InviteId;
    public readonly string InviteFromUserId;
    public readonly string InviteFromUserNickname;
    public readonly string InvitedUserId;
    
    public UserFriendInviteModel(string inviteFromUserId, string inviteFromUserNickname, string invitedUserId)
    {
        InviteId = Guid.NewGuid().ToString();
        InviteFromUserId = inviteFromUserId;
        InviteFromUserNickname = inviteFromUserNickname;
        InvitedUserId = invitedUserId;
    }

    public void Accept()
    {
        OnDecided?.Invoke(true);
    }

    public void Decline()
    {
        OnDecided?.Invoke(false);
    }
}