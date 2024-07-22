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
        
        Logger.Instance.Log($"Party invite: {_model.InviteId} enable timer!");
    }

    public void Dispose()
    {
        _timer.Stop();
        _model.OnDecided -= HandleDecision;
        
        Logger.Instance.Log($"Party invite: {_model.InviteId} has been disposed!");
    }
    
    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _gameModel.PartyInviteCollection.RemoveInvite(_model.InviteId);
        Logger.Instance.Log($"Party invite: {_model.InviteId} has been disposed by timer!");
    }

    private void HandleDecision(bool result)
    {
        _timer.Stop();

        if (!_gameModel.UsersCollection.TryGetUser(_model.InviteFromUserId, out var fromUser)) return;
        if (!_gameModel.UsersCollection.TryGetUser(_model.InvitedUserId, out var invitedUser)) return;

        if (result)
        {
            if (!fromUser.UserData.PartyData.InParty.Value)
            {
                var newPartyModel = _gameModel.PartiesCollection.Create(fromUser.PlayerId, fromUser.PlayerNickname);

                fromUser.UserData.PartyData.Guid.Value = newPartyModel.Guid;
                fromUser.UserData.PartyData.OwnerId.Value = newPartyModel.OwnerId;
                fromUser.UserData.PartyData.OwnerNickname.Value = newPartyModel.OwnerNickname;
                fromUser.UserData.PartyData.InParty.Value = true;

                invitedUser.UserData.PartyData.Guid.Value = newPartyModel.Guid;
                invitedUser.UserData.PartyData.OwnerId.Value = newPartyModel.OwnerId;
                invitedUser.UserData.PartyData.OwnerNickname.Value = newPartyModel.OwnerNickname;
                invitedUser.UserData.PartyData.InParty.Value = true;
             
                
                newPartyModel.AddMember(fromUser.PlayerNickname);
                newPartyModel.AddMember(invitedUser.PlayerNickname);
                
                //TODO: causes bug, need to fix
                // invitedUser.Invites.Remove(_model.InviteId);
            
                Logger.Instance.Log($"Party: {newPartyModel.Guid} created from user: {fromUser.PlayerId}");
                Logger.Instance.Log($"User: {invitedUser.PlayerId} accepted invite and added to party: {newPartyModel.Guid}");
            }
        }
        else
        {
            invitedUser.UserData.PartyInvites.Remove(_model.InviteId);
            
            _gameModel.PartyInviteCollection.RemoveInvite(_model.InviteId);

            Logger.Instance.Log($"User: {invitedUser.PlayerId} declined invite: {_model.InviteId}");
        }
    }
}