using ServerCore.Main;
using ServerCore.Main.Commands.Party;
using ServerCore.Main.Party;
using ServerCore.Main.Utilities.Logger;

namespace Server.CommandExecutors.Variants.Party;

public class InvitePartyCommandExecutor : CommandExecutor<InvitePartyCommand>
{
    public InvitePartyCommandExecutor(InvitePartyCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.FromUserId, out var fromUser)) return;
        if (!GameModel.UsersCollection.TryGetUser(Command.InvitedUserId, out var invitedUser)) return;
        if (fromUser.Invites.Collection.ContainsKey(invitedUser.PlayerId.Value)) return;

        PartyInviteData inviteData;
        
        //проверить у отправляющего игрока существование пати
        if (fromUser.PartyData.InParty.Value)
        {
            //если есть, то достаем это пати из коллекции пати и добавляем туда инвайт приглашенному игроку
            if (!GameModel.PartiesCollection.TryGetParty(fromUser.PartyData.Guid.Value, out var party)) return;
            
            // inviteData = party.CreateInvite(fromUser.PlayerId.Value, invitedUser.PlayerId.Value);

            inviteData = new PartyInviteData();
            
            Logger.Instance.Log($"User {fromUser.PlayerId.Value} already in party: {party.Guid}");
        }
        else
        {
            var inviteModel = GameModel.PartyInviteCollection.CreateInvite(fromUser.PlayerId.Value, fromUser.PlayerNickname.Value ,invitedUser.PlayerId.Value);
            
            inviteData = new PartyInviteData
            {
                InviteId = { Value = inviteModel.InviteId },
                InviteFromUserId = { Value = inviteModel.InviteFromUserId },
                InviteFromUserNickname = { Value = inviteModel.InviteFromUserNickname },
                InvitedUserId = { Value = inviteModel.InvitedUserId }
            };
        }
        
        //приглашенному игроку в патиинвайтдату этот инвайт
        invitedUser.Invites.Add(inviteData.InviteId.Value, inviteData);
    }
}