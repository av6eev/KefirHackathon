using ServerCore.Main;
using ServerCore.Main.Commands.Friends;

namespace Server.CommandExecutors.Variants.Friends;

public class RemoveFriendCommandExecutor : CommandExecutor<RemoveFriendCommand>
{
    public RemoveFriendCommandExecutor(RemoveFriendCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        
    }
}