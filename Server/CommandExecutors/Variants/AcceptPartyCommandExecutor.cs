using ServerCore.Main;
using ServerCore.Main.Commands.Party;

namespace Server.CommandExecutors.Variants;

public class AcceptPartyCommandExecutor : CommandExecutor<AcceptPartyCommand>
{
    public AcceptPartyCommandExecutor(AcceptPartyCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.UserId, out var invitedUser)) return;
        if (!GameModel.PartiesCollection.TryGetParty(Command.PartyId, out var party)) return;
        if (!GameModel.UsersCollection.TryGetUser(party.PartyData.OwnerId.Value, out var partyOwner)) return;

        var inviteData = invitedUser.Invites.Collection[party.PartyData.Id];
        inviteData.SetResult(true);
        
        party.RemoveInvite(invitedUser.PlayerId.Value);
        party.AddMember(invitedUser.PlayerId.Value);
        
        invitedUser.Invites.Remove(party.PartyData.Id);
        invitedUser.PartyData = partyOwner.PartyData;
    }
}