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
            if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var userModel)) continue;
            
            userModel.UserData.PartyData.Members.Clear();
            userModel.UserData.PartyData.InParty.Value = false;
            userModel.UserData.PartyData.Guid.Value = string.Empty;
            userModel.UserData.PartyData.OwnerId.Value = string.Empty;
            userModel.UserData.PartyData.OwnerNickname.Value = string.Empty;
        }
        
        _model.OnMemberAdded -= HandleMemberAdd;
        _model.OnMemberRemoved -= HandleMemberRemove;
        _model.OnLeaderChange -= HandleLeaderChange;
        
        Logger.Instance.Log($"Party: {_model.Guid} disposed with members count: {_model.Members.Count}!");
    }

    private void HandleLeaderChange(string newLeaderNickname)
    {
        if (!_gameModel.UsersCollection.TryGetUserByNickname(newLeaderNickname, out var newLeaderUserModel)) return;

        foreach (var memberNickname in _model.Members)
        {
            if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserModel)) continue;
            
            memberUserModel.UserData.PartyData.OwnerNickname.Value = newLeaderNickname;
            memberUserModel.UserData.PartyData.OwnerId.Value = newLeaderUserModel.PlayerId;
        }
        
        Logger.Instance.Log($"Party: {_model.Guid} new leader nickname: {newLeaderNickname}.");
    }

    private void HandleMemberAdd(string userNickname)
    {
        if (!_gameModel.UsersCollection.TryGetUserByNickname(userNickname, out var addedUserModel)) return;
        
        foreach (var memberNickname in _model.Members)
        {
            addedUserModel.UserData.PartyData.Members.Add(memberNickname);
            
            if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserModel)) continue;
            if (memberUserModel.UserData.PartyData.Members.Contains(userNickname)) continue;
            
            memberUserModel.UserData.PartyData.Members.Add(userNickname);
        }
    }

    private void HandleMemberRemove(string userNickname)
    {
        if (!_gameModel.UsersCollection.TryGetUserByNickname(userNickname, out var removedUserModel)) return;
        
        removedUserModel.UserData.PartyData.Members.Clear();
        removedUserModel.UserData.PartyData.InParty.Value = false;
        removedUserModel.UserData.PartyData.Guid.Value = string.Empty;
        removedUserModel.UserData.PartyData.OwnerId.Value = string.Empty;
        removedUserModel.UserData.PartyData.OwnerNickname.Value = string.Empty;
        
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
                        if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserModel)) continue;
                        memberUserModel.UserData.PartyData.Members.Remove(userNickname);
                    }
                    
                    _model.ChangeLeader(_model.Members[new Random().Next(0, _model.Members.Count)]);
                    break;
            }
        
            Logger.Instance.Log($"User with nickname: {userNickname} removed from party: {_model.Guid}");
        }
    }
}