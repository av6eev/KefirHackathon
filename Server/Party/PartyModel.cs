using ServerCore.Main.Party;

namespace Server.Party;

public class PartyModel
{
    public event Action<PartyInviteData> OnInviteCreated; 
    public event Action<string> OnInviteRemoved;
    public event Action<string> OnMemberAdded; 
    public event Action<string> OnMemberRemoved;

    public PartyData PartyData { get; }
    
    public PartyModel(string id, string ownerId)
    {
        PartyData = new PartyData(id)
        {
            OwnerId = { Value = ownerId },
            InParty = { Value = true }
        };
        
        PartyData.Members.Add(ownerId);
    }

    public PartyInviteData CreateInvite(string partyOwnerId, string invitedUserId)
    {
        var inviteData = new PartyInviteData
        {
            InviteId = { Value = Guid.NewGuid().ToString() },
            InviteFromUserId = { Value = partyOwnerId },
            InvitedUserId = { Value = invitedUserId },
            InvitedPartyId = { Value = PartyData.Id }
        };

        PartyData.Invites.Add(invitedUserId, inviteData);
        OnInviteCreated?.Invoke(inviteData);
        
        return inviteData;
    }

    public void RemoveInvite(string invitedUserId)
    {
        if (!PartyData.Invites.Collection.ContainsKey(invitedUserId)) return;
        
        PartyData.Invites.Remove(invitedUserId);
        OnInviteRemoved?.Invoke(invitedUserId);
    }

    public void AddMember(string userId)
    {
        if (PartyData.Members.Contains(userId)) return;
        
        PartyData.Members.Add(userId);
        OnMemberAdded?.Invoke(userId);
    }

    public void RemoveMember(string userId)
    {
        if (!PartyData.Members.Contains(userId)) return;

        PartyData.Members.Remove(userId);
        OnMemberRemoved?.Invoke(userId);
    }
}