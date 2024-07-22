using ServerCore.Main;
using ServerCore.Main.Commands.Friends;

namespace Server.CommandExecutors.Variants.Friends;

public class DeclineFriendCommandExecutor : CommandExecutor<DeclineFriendCommand>
{
    public DeclineFriendCommandExecutor(DeclineFriendCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.UserId, out var invitedUser)) return;

        if (invitedUser.FriendsCollection.TryGetInvite(Command.InviteId, out var inviteModel))
        {
            inviteModel.Decline();
        }
        
        // invitedUser.UserData.FriendInvites.Remove(Command.InviteId);
    }
}