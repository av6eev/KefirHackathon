using Entities.Enemy.Collection;
using Entities.Player;
using InteractiveObjects.Portal;

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
            var enemySpecification = _gameModel.Specifications.EnemySpecifications["test_enemy"];
            
            _gameModel.EnemiesCollection.AddEnemy(enemySpecification, _gameModel.PlayerModel);
            _gameModel.EnemiesCollection.AddEnemy(enemySpecification, _gameModel.PlayerModel);
            _gameModel.EnemiesCollection.AddEnemy(enemySpecification, _gameModel.PlayerModel);
            
            Presenters.Add(new EnemiesCollectionPresenter(GameModel, (EnemiesCollection)_gameModel.EnemiesCollection, _view.EnemiesCollectionView));
            Presenters.Add(new PortalPresenter(_gameModel, _view.PortalView));

            _playerAfkUpdater = new PlayerAfkUpdater((PlayerModel) _gameModel.PlayerModel); 
            _gameModel.UpdatersList.Add(_playerAfkUpdater);            
        }

        protected override void AfterDispose()
        {
            _gameModel.UpdatersList.Remove(_playerAfkUpdater);
        }
    }
}