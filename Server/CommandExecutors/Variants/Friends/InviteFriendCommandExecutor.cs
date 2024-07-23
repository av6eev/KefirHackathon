using ServerCore.Main;
using ServerCore.Main.Commands.Friends;
using ServerCore.Main.Friends;
using ServerCore.Main.Utilities.Logger;

namespace Server.CommandExecutors.Variants.Friends;

public class InviteFriendCommandExecutor : CommandExecutor<InviteFriendCommand>
{
    public InviteFriendCommandExecutor(InviteFriendCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.FromUserId, out var fromUser)) return;
        if (!GameModel.UsersCollection.TryGetUser(Command.InvitedUserId, out var invitedUser)) return;
        if (fromUser.UserData.FriendInvites.Collection.ContainsKey(invitedUser.PlayerId)) return;
        
        if (fromUser.UserData.FriendsData.Friends.Contains(invitedUser.PlayerId))
        {
            Logger.Instance.Log($"User: {fromUser.PlayerId} and {invitedUser.PlayerId} are friends already!");    
            return;
        }
        
        var inviteModel = invitedUser.FriendsCollection.CreateFriendInvite(fromUser.PlayerId, fromUser.PlayerNickname, invitedUser.PlayerId);
        var inviteData = new FriendInviteData
        {
            InviteId = { Value = inviteModel.InviteId },
            InviteFromUserId = { Value = inviteModel.InviteFromUserId },
            InviteFromUserNickname = { Value = inviteModel.InviteFromUserNickname },
            InvitedUserId = { Value = inviteModel.InvitedUserId }
        };
        
        invitedUser.UserData.FriendInvites.Add(inviteData.InviteId.Value, inviteData);
    }
}