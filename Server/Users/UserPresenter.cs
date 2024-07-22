using Server.Users.Friends;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Users;

public class UserPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly UserModel _model;

    private readonly PresentersList _presenters = new();
    
    public UserPresenter(ServerGameModel gameModel, UserModel model)
    {
        _gameModel = gameModel;
        _model = model;
    }
    
    public void Init()
    {
        _gameModel.SaveSingleModelCollection.Add(_model);

        _presenters.Add(new UserFriendsCollectionPresenter(_gameModel, _model.FriendsCollection));
        _presenters.Init();
    }

    public void Dispose()
    {
        _gameModel.SaveSingleModelCollection.RemoveElement(_model.SaveId);

        _presenters.Dispose();
        _presenters.Clear();
        
        var world = _gameModel.WorldsCollection.Worlds[_model.WorldId];
        world.CharacterDataCollection.Remove(_model.PlayerId);

        if (_gameModel.PartiesCollection.TryGetParty(_model.UserData.PartyData.Guid.Value, out var partyModel))
        {
            partyModel.RemoveMember(_model.PlayerNickname);
        }
    }
}