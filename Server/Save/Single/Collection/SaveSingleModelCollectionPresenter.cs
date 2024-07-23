using ServerCore.Main.Utilities.Presenter;

namespace Server.Save.Single.Collection
{
    public class SaveSingleModelCollectionPresenter : IPresenter
    {
        private readonly ServerGameModel _gameModel;
        private readonly SaveSingleModelCollection _collection;

        private readonly PresentersDictionary<SaveSingleModel> _presenters = new();

        public SaveSingleModelCollectionPresenter(ServerGameModel gameModel, SaveSingleModelCollection collection)
        {
            _gameModel = gameModel;
            _collection = collection;
        }
        
        public void Init()
        {
            foreach (var model in _collection.GetModels())    
            {
                HandleAdd(model);
            }

            _collection.AddEvent.OnChanged += HandleAdd;
            _collection.RemoveEvent.OnChanged += HandleRemove;
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();
            
            _collection.AddEvent.OnChanged -= HandleAdd;
            _collection.RemoveEvent.OnChanged -= HandleRemove;
        }

        private void HandleAdd(SaveSingleModel model)
        {
            var presenter = new SaveSinglePresenter(_gameModel, _collection, model);
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        private void HandleRemove(SaveSingleModel model)
        {
            _presenters.Remove(model);
        }
    }
}