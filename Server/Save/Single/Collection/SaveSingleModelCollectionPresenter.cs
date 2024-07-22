using ServerCore.Main.Utilities.Presenter;

namespace Server.Save.Single.Collection
{
    public class SaveSingleModelCollectionPresenter : IPresenter
    {
        private readonly ServerGameModel _gameModel;
        private readonly SaveSingleModelCollection _collection;

        private readonly PresentersList _presenters = new();

        public SaveSingleModelCollectionPresenter(ServerGameModel gameModel, SaveSingleModelCollection collection)
        {
            _gameModel = gameModel;
            _collection = collection;
        }
        
        public void Init()
        {
            foreach (var model in _collection.GetModels())    
            {
                var presenter = new SaveSinglePresenter(_gameModel, _collection, model);
                presenter.Init();
                _presenters.Add(presenter);
            }

            _collection.AddEvent.OnChanged += HandleAdd;
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();
            
            _collection.AddEvent.OnChanged -= HandleAdd;
        }

        private void HandleAdd(SaveSingleModel model)
        {
            var presenter = new SaveSinglePresenter(_gameModel, _collection, model);
            presenter.Init();
            _presenters.Add(presenter);
        }
    }
}