using Server.Party.Observers;
using ServerCore.Main;
using ServerCore.Main.Commands;
using ServerCore.Main.Commands.Party;
using ServerCore.Main.Party;

namespace Server.CommandExecutors.Variants;

public class InvitePartyCommandExecutor : CommandExecutor<InvitePartyCommand>
{
    public InvitePartyCommandExecutor(InvitePartyCommand command, ServerGameModel gameModel, Peer peer) : base(command, gameModel, ref peer)
    {
    }

    public override void Execute()
    {
        if (!GameModel.UsersCollection.TryGetUser(Command.FromUserId, out var fromUser)) return;
        if (!GameModel.UsersCollection.TryGetUser(Command.InvitedUserId, out var invitedUser)) return;
        if (fromUser.PartyData.Invites.Collection.ContainsKey(invitedUser.PlayerId.Value)) return;

        PartyInviteData inviteData;
        
        //проверить у отправляющего игрока существование пати
        if (fromUser.PartyData.InParty.Value)
        {
            //если есть, то достаем это пати из коллекции пати и добавляем туда инвайт приглашенному игроку
            if (!GameModel.PartiesCollection.TryGetParty(fromUser.PartyData.Id, out var party)) return;
            
            inviteData = party.CreateInvite(fromUser.PlayerId.Value, invitedUser.PlayerId.Value);
        }
        else
        {
            //если нет, то запрашиваем создание пати у коллекции пати
            var newParty = GameModel.PartiesCollection.Create(fromUser.PlayerId.Value);
            
            inviteData = newParty.CreateInvite(fromUser.PlayerId.Value, invitedUser.PlayerId.Value);
            
            //добавляем туда инвайт приглашенному игроку
            newParty.PartyData.Invites.Add(invitedUser.PlayerId.Value, inviteData);
            
            fromUser.PartyData = newParty.PartyData;
        }
        
        //приглашенному игроку в патиинвайтдату этот инвайт
        invitedUser.Invites.Add(inviteData.InvitedPartyId.Value, inviteData);
    }
}