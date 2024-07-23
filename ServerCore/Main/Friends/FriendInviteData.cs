using ServerCore.Main.Property;

namespace ServerCore.Main.Friends
{
    public class FriendInviteData : ServerData
    {
        public readonly Property<string> InviteId = new("invite_id", string.Empty);
        public readonly Property<string> InviteFromUserId = new("invite_from_user_id", string.Empty);
        public readonly Property<string> InviteFromUserNickname = new("invite_from_user_nickname", string.Empty);
        public readonly Property<string> InvitedUserId = new("invited_user_id", string.Empty);
        
        public FriendInviteData() : base("friend_invite_data")
        {
            Properties.Add(InviteId.Id, InviteId);
            Properties.Add(InviteFromUserId.Id, InviteFromUserId);
            Properties.Add(InviteFromUserNickname.Id, InviteFromUserNickname);
            Properties.Add(InvitedUserId.Id, InvitedUserId);
        }
    }
}