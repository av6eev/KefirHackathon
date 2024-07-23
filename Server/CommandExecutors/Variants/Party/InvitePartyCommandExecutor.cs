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
        if (fromUser.UserData.PartyInvites.Collection.ContainsKey(invitedUser.PlayerId)) return;

        PartyInviteData inviteData;
        
        //проверить у отправляющего игрока существование пати
        if (fromUser.UserData.PartyData.InParty.Value)
        {
            //если есть, то достаем это пати из коллекции пати и добавляем туда инвайт приглашенному игроку
            if (!GameModel.PartiesCollection.TryGetParty(fromUser.UserData.PartyData.Guid.Value, out var party)) return;
            
            // inviteData = party.CreateInvite(fromUser.PlayerId.Value, invitedUser.PlayerId.Value);

            inviteData = new PartyInviteData();
            
            Logger.Instance.Log($"User {fromUser.PlayerId} already in party: {party.Guid}");
        }
        else
        {
            var inviteModel = GameModel.PartyInviteCollection.CreateInvite(fromUser.PlayerId, fromUser.PlayerNickname, invitedUser.PlayerId);
            
            inviteData = new PartyInviteData
            {
                InviteId = { Value = inviteModel.InviteId },
                InviteFromUserId = { Value = inviteModel.InviteFromUserId },
                InviteFromUserNickname = { Value = inviteModel.InviteFromUserNickname },
                InvitedUserId = { Value = inviteModel.InvitedUserId }
            };
        }
        
        //приглашенному игроку в патиинвайтдату этот инвайт
        invitedUser.UserData.PartyInvites.Add(inviteData.InviteId.Value, inviteData);
    }
}