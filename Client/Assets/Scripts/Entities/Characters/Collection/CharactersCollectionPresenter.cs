using Presenter;

namespace Entities.Characters.Collection
{
    public class CharactersCollectionPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly CharactersCollection _model;
        private readonly CharactersCollectionView _view;

        private readonly PresentersDictionary<CharacterModel> _presenters = new();
        
        public CharactersCollectionPresenter(IGameModel gameModel, CharactersCollection model, CharactersCollectionView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
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
            _model.AddEvent.OnChanged -= HandleAdd;
            _model.RemoveEvent.OnChanged -= HandleRemove;
        }

        private void HandleAdd(CharacterModel model)
        {
            // if (model.Id == _gameModel.PlayerModel.Id)
            // {
            //     return;
            // }
            
            var presenter = new CharacterPresenter(_gameModel, model, _view.Root);
            presenter.Init();
            
            _presenters.Add(model, presenter);
        }

        private void HandleRemove(CharacterModel model)
        {
            if (model.Id == _gameModel.PlayerModel.Id)
            {
                return;
            }
            
            //TODO: remove
        }
    }
}