using ServerCore.Main.Utilities.Logger;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Users.Collection;

public class UsersCollectionPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly UsersCollection _model;

    private readonly PresentersDictionary<UserModel> _userPresenters = new();
    
    public UsersCollectionPresenter(ServerGameModel gameModel, UsersCollection model)
    {
        _gameModel = gameModel;
        _model = model;
    }
    
    public void Init()
    {
        _model.AddEvent.OnChanged += HandleAdd;
        _model.RemoveEvent.OnChanged += HandleRemove;
    }

    public void Dispose()
    {
        _userPresenters.Dispose();
        _userPresenters.Clear();
        
        _model.AddEvent.OnChanged -= HandleAdd;
        _model.RemoveEvent.OnChanged -= HandleRemove;
    }

    private void HandleAdd(UserModel model)
    {
        var presenter = new UserPresenter(_gameModel, model);
        presenter.Init();
        
        _userPresenters.Add(model, presenter);
    }

    private void HandleRemove(UserModel model)
    {
        _userPresenters.Remove(model);
        Logger.Instance.Log($"User: {model.PlayerNickname} disconnected!");
    }
}