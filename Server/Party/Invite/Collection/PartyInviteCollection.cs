namespace Server.Party.Invite.Collection;

public class PartyInviteCollection : IPartyInviteCollection
{
    public event Action<PartyInviteModel> OnInviteCreated; 
    public event Action<PartyInviteModel> OnInviteRemoved;
    
    public readonly Dictionary<string, PartyInviteModel> Invites = new();

    public PartyInviteModel CreateInvite(string inviteFromUserId, string inviteFromUserNickname, string invitedUserId)
    {
        var inviteModel = new PartyInviteModel(inviteFromUserId, inviteFromUserNickname, invitedUserId);
        
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

    public PartyInviteModel GetInvite(string inviteId)
    {
        return Invites.GetValueOrDefault(inviteId);
    }

    public bool TryGetConcreteInvite(string fromUserId, string toUserId, out PartyInviteModel inviteModel)
    {
        foreach (var invite in Invites)
        {
            if (invite.Key == fromUserId && invite.Value.InvitedUserId == toUserId)
            {
                inviteModel = invite.Value;
                return true;
            }
        }

        inviteModel = null;
        return false;
    }
}