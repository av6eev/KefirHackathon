using Entities.Characters.Collection;
using InteractiveObjects.Portal;
using UnityEngine;

namespace GameScenes.TestConnection
{
    public class TestConnectionScenePresenter : BaseGameScenePresenter
    {
        private readonly GameModel _gameModel;
        private readonly TestConnectionSceneView _view;

        public TestConnectionScenePresenter(GameModel gameModel, TestConnectionSceneView view) : base(gameModel, view)
        {
            _gameModel = gameModel;
            _view = view;
        }

        protected override void AfterInit()
        {
            Presenters.Add(new CharactersCollectionPresenter(_gameModel, _gameModel.CharactersCollection, _view.CharactersCollectionView));
            Presenters.Add(new PortalPresenter(_gameModel, _view.PortalView));
        }

        protected override void AfterDispose()
        {
        }
    }
}