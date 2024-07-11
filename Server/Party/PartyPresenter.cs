using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Party;

public class PartyPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly PartyModel _model;

    public PartyPresenter(ServerGameModel gameModel, PartyModel model)
    {
        _gameModel = gameModel;
        _model = model;
    }

    public void Init()
    {
        _model.OnMemberAdded += HandleMemberAdd;
        _model.OnMemberRemoved += HandleMemberRemove;
        _model.OnLeaderChange += HandleLeaderChange;
    }

    public void Dispose()
    {
        foreach (var memberNickname in _model.Members)
        {
            if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserData)) continue;
            
            memberUserData.PartyData.Members.Clear();
            memberUserData.PartyData.InParty.Value = false;
            memberUserData.PartyData.Guid.Value = string.Empty;
            memberUserData.PartyData.OwnerId.Value = string.Empty;
            memberUserData.PartyData.OwnerNickname.Value = string.Empty;
        }
        
        _model.OnMemberAdded -= HandleMemberAdd;
        _model.OnMemberRemoved -= HandleMemberRemove;
        _model.OnLeaderChange -= HandleLeaderChange;
        
        Logger.Instance.Log($"Party: {_model.Guid} disposed with members count: {_model.Members.Count}!");
    }

    private void HandleLeaderChange(string newLeaderNickname)
    {
        if (!_gameModel.UsersCollection.TryGetUserByNickname(newLeaderNickname, out var newLeaderUserData)) return;

        foreach (var memberNickname in _model.Members)
        {
            if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserData)) continue;
            
            memberUserData.PartyData.OwnerNickname.Value = newLeaderNickname;
            memberUserData.PartyData.OwnerId.Value = newLeaderUserData.PlayerId.Value;
        }
        
        Logger.Instance.Log($"Party: {_model.Guid} new leader nickname: {newLeaderNickname}.");
    }

    private void HandleMemberAdd(string userNickname)
    {
        if (!_gameModel.UsersCollection.TryGetUserByNickname(userNickname, out var addedUserData)) return;
        
        foreach (var memberNickname in _model.Members)
        {
            addedUserData.PartyData.Members.Add(memberNickname);
            
            if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserData)) continue;
            if (memberUserData.PartyData.Members.Contains(userNickname)) continue;
            
            memberUserData.PartyData.Members.Add(userNickname);
        }
    }

    private void HandleMemberRemove(string userNickname)
    {
        if (!_gameModel.UsersCollection.TryGetUserByNickname(userNickname, out var removedUserData)) return;
        
        removedUserData.PartyData.Members.Clear();
        removedUserData.PartyData.InParty.Value = false;
        removedUserData.PartyData.Guid.Value = string.Empty;
        removedUserData.PartyData.OwnerId.Value = string.Empty;
        removedUserData.PartyData.OwnerNickname.Value = string.Empty;
        
        if (userNickname == _model.OwnerNickname)
        {
            _gameModel.PartiesCollection.Remove(_model.Guid);
            Logger.Instance.Log($"User with nickname: {userNickname} initiated party dispose!");
        }
        else
        {
            switch (_model.Members.Count)
            {
                case 1:
                    _gameModel.PartiesCollection.Remove(_model.Guid);
                    break;
                case > 1:
                    foreach (var memberNickname in _model.Members)
                    {
                        if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserData)) continue;
                        memberUserData.PartyData.Members.Remove(userNickname);
                    }
                    
                    _model.ChangeLeader(_model.Members[new Random().Next(0, _model.Members.Count)]);
                    break;
            }
        
            Logger.Instance.Log($"User with nickname: {userNickname} removed from party: {_model.Guid}");
        }
    }
}