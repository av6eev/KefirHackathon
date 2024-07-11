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
    }

    public void Dispose()
    {
        _model.OnMemberAdded -= HandleMemberAdd;
        _model.OnMemberRemoved -= HandleMemberRemove;
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
        foreach (var memberNickname in _model.Members)
        {
            if (!_gameModel.UsersCollection.TryGetUserByNickname(memberNickname, out var memberUserData)) continue;
            memberUserData.PartyData.Members.Remove(userNickname);
        }
    }
}