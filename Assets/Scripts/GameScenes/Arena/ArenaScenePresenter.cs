using Entities.Enemy.Collection;
using InteractiveObjects.Portal;

namespace GameScenes.Arena
{
    public class ArenaScenePresenter : BaseGameScenePresenter
    {
        private readonly GameModel _gameModel;
        private readonly ArenaSceneView _view;

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
        }

        protected override void AfterDispose()
        {
        }
    }
}