namespace Server.Party.Invite.Collection;

public interface IPartyInviteCollection
{
    PartyInviteModel CreateInvite(string inviteFromUserId, string inviteFromUserNickname, string invitedUserId);
    void RemoveInvite(string inviteId);
    bool TryGetConcreteInvite(string fromUserId, string toUserId, out PartyInviteModel inviteModel);
    PartyInviteModel GetInvite(string inviteId);
}