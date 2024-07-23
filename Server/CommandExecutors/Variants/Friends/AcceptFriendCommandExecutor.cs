﻿using ServerCore.Main;
using ServerCore.Main.Commands.Friends;

namespace Server.CommandExecutors.Variants.Friends;

public class AcceptFriendCommandExecutor : CommandExecutor<AcceptFriendCommand>
{
    public AcceptFriendCommandExecutor(AcceptFriendCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.UserId, out var invitedUser)) return;

        if (invitedUser.FriendsCollection.TryGetInvite(Command.InviteId, out var inviteModel))
        {
            inviteModel.Accept();
        }
        
        // invitedUser.UserData.FriendInvites.Remove(Command.InviteId);
    }
}