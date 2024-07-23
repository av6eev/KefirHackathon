using ServerCore.Main.Utilities.Presenter;

namespace Server.Party.Collection;

public class PartiesCollectionPresenter : IPresenter
{
    private readonly ServerGameModel _gameModel;
    private readonly PartiesCollection _collection;

    private readonly PresentersDictionary<PartyModel> _partyPresenters = new();
    
    public PartiesCollectionPresenter(ServerGameModel gameModel, PartiesCollection collection)
    {
        _gameModel = gameModel;
        _collection = collection;
    }
    
    public void Init()
    {
        _collection.OnCreated += HandleAdd;
        _collection.OnRemoved += HandleRemove;
    }

    public void Dispose()
    {
        _collection.OnCreated -= HandleAdd;
        _collection.OnRemoved -= HandleRemove;
    }

    private void HandleAdd(PartyModel model)
    {
        var presenter = new PartyPresenter(_gameModel, model);
        presenter.Init();
        
        _partyPresenters.Add(model, presenter);
    }

    private void HandleRemove(PartyModel model)
    {
        _partyPresenters.Remove(model);
    }
}