using Server.Services.Friends;
using ServerCore.Main.Utilities.Presenter;

namespace Server.Services.Collection;

public class ServicesCollectionPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly ServicesCollection _model;

    private readonly PresentersDictionary<IServiceModel> _servicePresenters = new();
    
    public ServicesCollectionPresenter(ServerGameModel gameModel, ServicesCollection model)
    {
        _gameModel = gameModel;
        _model = model;
    }
    
    public void Init()
    {
        foreach (var model in _model.GetModels())
        {
            HandleAdd(model);
        }
        
        _model.AddEvent.OnChanged += HandleAdd;
        _model.RemoveEvent.OnChanged += HandleRemove;
    }

    public void Dispose()
    {
        _servicePresenters.Dispose();
        _servicePresenters.Clear();
        
        _model.AddEvent.OnChanged -= HandleAdd;
        _model.RemoveEvent.OnChanged -= HandleRemove;
    }

    private void HandleAdd(IServiceModel model)
    {
        IPresenter presenter = null;
        
        switch (model.Type)
        {
            case ServiceType.Friends:
                presenter = new FriendsServiceObserver(_gameModel, (FriendsServiceModel)model);
                break;
        }
        
        presenter?.Init();
        _servicePresenters.Add(model, presenter);
    }

    private void HandleRemove(IServiceModel model)
    {
        _servicePresenters.Remove(model);
    }
}