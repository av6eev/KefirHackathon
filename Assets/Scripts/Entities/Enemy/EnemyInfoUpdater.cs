using Updater;

namespace Entities.Enemy
{
    public class EnemyInfoUpdater : IUpdater
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;

        public EnemyInfoUpdater(EnemyModel model, EnemyView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Update(float deltaTime)
        {
            _model.Position = _view.Position;
        }
    }
}