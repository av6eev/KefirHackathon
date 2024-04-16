using System.Linq;
using Presenter;
using ServerCore.Main;
using UnityEngine;

namespace Entities.Characters.Collection
{
    public class CharactersCollectionPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly CharactersCollection _model;
        private readonly CharactersCollectionView _view;

        private readonly PresentersDictionary<string> _presenters = new();
        
        public CharactersCollectionPresenter(IGameModel gameModel, CharactersCollection model, CharactersCollectionView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _gameModel.WorldData.CharacterDataCollection.OnAdd += HandleAdd;
            _gameModel.WorldData.CharacterDataCollection.OnRemove += HandleRemove;
        }

        public void Dispose()
        {
            _gameModel.WorldData.CharacterDataCollection.OnAdd -= HandleAdd;
            _gameModel.WorldData.CharacterDataCollection.OnRemove -= HandleRemove;
        }

        private void HandleAdd(CharacterServerData serverData)
        {
            if (serverData.PlayerId.Value == _gameModel.PlayerModel.Id)
            {
                return;
            }
            
            var presenter = new CharacterPresenter(_gameModel, _model.AddCharacter(serverData, _gameModel.Specifications.EntitySpecifications.GetSpecifications().Values.First()), _view.Root);
            presenter.Init();
            
            _presenters.Add(serverData.PlayerId.Value, presenter);
        }

        private void HandleRemove(string playerId)
        {
            if (playerId == _gameModel.PlayerModel.Id)
            {
                return;
            }

            _presenters.Remove(playerId);
            
            Debug.Log($"removed: {playerId}");
        }
    }
}