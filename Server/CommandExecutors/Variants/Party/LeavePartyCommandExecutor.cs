using ServerCore.Main;
using ServerCore.Main.Commands.Party;

namespace Server.CommandExecutors.Variants.Party;

public class LeavePartyCommandExecutor : CommandExecutor<LeavePartyCommand>
{
    public LeavePartyCommandExecutor(LeavePartyCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.UserId, out var userData)) return;
        if (!GameModel.PartiesCollection.TryGetParty(Command.PartyId, out var partyModel)) return;
        
        partyModel.RemoveMember(userData.PlayerNickname);
    }
}