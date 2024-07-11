using ServerCore.Main;
using ServerCore.Main.Commands.Party;

namespace Server.CommandExecutors.Variants.Party;

public class DeclinePartyCommandExecutor : CommandExecutor<DeclinePartyCommand>
{
    public DeclinePartyCommandExecutor(DeclinePartyCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        var inviteModel = GameModel.PartyInviteCollection.GetInvite(Command.InviteId);
        inviteModel.Decline();
    }
}