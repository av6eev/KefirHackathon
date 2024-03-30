using Updater;

namespace Entities.Enemy.State
{
    public class EnemyAttackStateUpdater : IUpdater
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;

        public EnemyAttackStateUpdater(EnemyModel model, EnemyView view)
        {
            _model = model;
            _view = view;
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}