namespace Server.Party.Invite;

public class PartyInviteModel
{
    public event Action<bool> OnDecided;

    public readonly string InviteId;
    public readonly string InviteFromUserId;
    public readonly string InviteFromUserNickname;
    public readonly string InvitedUserId;
    
    public PartyInviteModel(string inviteFromUserId, string inviteFromUserNickname, string invitedUserId)
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