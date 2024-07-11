using Entities.Enemy.Collection;
using Entities.Enemy.Specification;
using Entities.Player;
using GameScenes.GameUI.DeBuffPanel;
using InteractiveObjects.Portal;
using LoadingScreen;
using SceneManagement;
using UnityEngine;

namespace GameScenes.Arena
{
    public class ArenaScenePresenter : BaseGameScenePresenter
    {
        private readonly GameModel _gameModel;
        private readonly ArenaSceneView _view;
        
        private PlayerAfkUpdater _playerAfkUpdater;
        
        public ArenaScenePresenter(GameModel gameModel, ArenaSceneView view) : base(gameModel, view)
        {
            _gameModel = gameModel;
            _view = view;
        }

        protected override void AfterInit()
        {
            _gameModel.SceneManagementModelsCollection.SetCurrentSceneId(SceneConst.ArenaId);

            for (var i = 0; i < EnemiesCollection.MaxEnemiesCount; i++)
            {
                var randomIndex = Random.Range(0, 1);
                EnemySpecification enemySpecification = null;

                switch (randomIndex)
                {
                    case 0:
                        enemySpecification = _gameModel.Specifications.EnemySpecifications["mage_one"];
                        break;
                    case 1:
                        enemySpecification = _gameModel.Specifications.EnemySpecifications["mage_two"];
                        break;
                }
                
                _gameModel.EnemiesCollection.AddEnemy(enemySpecification, _gameModel.PlayerModel);
            }
            
            Presenters.Add(LoadingScreenMessageConst.EnemiesCollectionPresenter, new EnemiesCollectionPresenter(GameModel, (EnemiesCollection)_gameModel.EnemiesCollection, _view.EnemiesCollectionView));
            Presenters.Add(LoadingScreenMessageConst.PortalPresenter, new PortalPresenter(_gameModel, _view.PortalView));
            Presenters.Add(LoadingScreenMessageConst.DeBuffPenaltyPresenter, new DeBuffPenaltyPresenter(_gameModel));

            _gameModel.QuestsCollection.AddQuest("first_quest");
            
            _playerAfkUpdater = new PlayerAfkUpdater((PlayerModel) _gameModel.PlayerModel); 
            _gameModel.UpdatersList.Add(_playerAfkUpdater);            
        }

        protected override void AfterDispose()
        {
            _gameModel.UpdatersList.Remove(_playerAfkUpdater);
        }
    }
}