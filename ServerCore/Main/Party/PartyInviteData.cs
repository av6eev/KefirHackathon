using System;
using ServerCore.Main.Property;

namespace ServerCore.Main.Party
{
    public class PartyInviteData : ServerData
    {
        public readonly Property<string> InviteId = new("invite_id", string.Empty);
        public readonly Property<string> InvitedPartyId = new("invited_party_id", string.Empty);
        public readonly Property<string> InviteFromUserId = new("invite_from_user_id", string.Empty);
        public readonly Property<string> InviteFromUserNickname = new("invite_from_user_nickname", string.Empty);
        public readonly Property<string> InvitedUserId = new("invited_user_id", string.Empty);
        
        public PartyInviteData() : base("party_invite_data")
        {
            Properties.Add(InviteId.Id, InviteId);
            Properties.Add(InvitedPartyId.Id, InvitedPartyId);
            Properties.Add(InviteFromUserId.Id, InviteFromUserId);
            Properties.Add(InviteFromUserNickname.Id, InviteFromUserNickname);
            Properties.Add(InvitedUserId.Id, InvitedUserId);
        }
    }
}