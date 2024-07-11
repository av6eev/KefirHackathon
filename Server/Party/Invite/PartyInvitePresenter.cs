using System.Timers;
using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;
using Timer = System.Timers.Timer;

namespace Server.Party.Invite;

public class PartyInvitePresenter : IPresenter
{
    private const float SecondsToWait = 10f;
    
    private readonly ServerGameModel _gameModel;
    private readonly PartyInviteModel _model;
    
    private Timer _timer;

    public PartyInvitePresenter(ServerGameModel gameModel, PartyInviteModel model)
    {
        _gameModel = gameModel;
        _model = model;
    }
    
    public void Init()
    {
        _model.OnDecided += HandleDecision;

        _timer = new Timer(SecondsToWait * 1000f);
        _timer.Elapsed += HandleTimerElapsed;
        _timer.AutoReset = false;
        _timer.Start();
        
        Logger.Instance.Log($"Invite: {_model.InviteId} enable timer!");
    }

    public void Dispose()
    {
        _timer.Stop();
        _model.OnDecided -= HandleDecision;
        
        Logger.Instance.Log($"Invite: {_model.InviteId} has been disposed!");
    }
    
    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _gameModel.PartyInviteCollection.RemoveInvite(_model.InviteId);
        Logger.Instance.Log($"Party: {_model.InviteId} has been disposed by timer!");
    }

    private void HandleDecision(bool result)
    {
        _timer.Stop();

        if (!_gameModel.UsersCollection.TryGetUser(_model.InviteFromUserId, out var fromUser)) return;
        if (!_gameModel.UsersCollection.TryGetUser(_model.InvitedUserId, out var invitedUser)) return;

        if (result)
        {
            if (!fromUser.PartyData.InParty.Value)
            {
                var newPartyModel = _gameModel.PartiesCollection.Create(fromUser.PlayerId.Value, fromUser.PlayerNickname.Value);

                fromUser.PartyData.Guid.Value = newPartyModel.Guid;
                fromUser.PartyData.OwnerId.Value = newPartyModel.OwnerId;
                fromUser.PartyData.OwnerNickname.Value = newPartyModel.OwnerNickname;
                fromUser.PartyData.InParty.Value = true;
                // fromUser.PartyData.Members.Add(fromUser.PlayerNickname.Value);
                // fromUser.PartyData.Members.Add(invitedUser.PlayerNickname.Value);

                invitedUser.PartyData.Guid.Value = newPartyModel.Guid;
                invitedUser.PartyData.OwnerId.Value = newPartyModel.OwnerId;
                invitedUser.PartyData.OwnerNickname.Value = newPartyModel.OwnerNickname;
                invitedUser.PartyData.InParty.Value = true;
                // invitedUser.PartyData.Members.Add(fromUser.PlayerNickname.Value);
                // invitedUser.PartyData.Members.Add(invitedUser.PlayerNickname.Value);

                // foreach (var nickname in newPartyModel.Members) 
                // {
                //     fromUser.PartyData.Members.Add(nickname);
                //     invitedUser.PartyData.Members.Add(nickname);
                // }
                
                newPartyModel.AddMember(fromUser.PlayerNickname.Value);
                newPartyModel.AddMember(invitedUser.PlayerNickname.Value);
                
                //TODO: causes bug, need to fix
                // invitedUser.Invites.Remove(_model.InviteId);
            
                Logger.Instance.Log($"Party: {newPartyModel.Guid} created from user: {fromUser.PlayerId.Value}");
                Logger.Instance.Log($"User: {invitedUser.PlayerId.Value} accepted invite and added to party: {newPartyModel.Guid}");
            }
        }
        else
        {
            invitedUser.Invites.Remove(_model.InviteId);
            
            _gameModel.PartyInviteCollection.RemoveInvite(_model.InviteId);

            Logger.Instance.Log($"User: {invitedUser.PlayerId.Value} declined invite: {_model.InviteId}");
        }
    }
}