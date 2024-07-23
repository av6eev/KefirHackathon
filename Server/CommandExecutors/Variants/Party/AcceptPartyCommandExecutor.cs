using ServerCore.Main;
using ServerCore.Main.Commands.Party;

namespace Server.CommandExecutors.Variants.Party;

public class AcceptPartyCommandExecutor : CommandExecutor<AcceptPartyCommand>
{
    public AcceptPartyCommandExecutor(AcceptPartyCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        var inviteModel = GameModel.PartyInviteCollection.GetInvite(Command.InviteId);
        inviteModel.Accept();
    }
}