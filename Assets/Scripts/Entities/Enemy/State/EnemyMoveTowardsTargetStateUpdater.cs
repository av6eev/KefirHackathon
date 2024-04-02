using Updater;

namespace Entities.Enemy.State
{
    public class EnemyMoveTowardsTargetStateUpdater : IUpdater
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;

        public EnemyMoveTowardsTargetStateUpdater(EnemyModel model, EnemyView view)
        {
            _model = model;
            _view = view;
        }

        public void Update(float deltaTime)
        {
            _view.NavMeshAgent.SetDestination(_model.Target.Value.Position);
        }
    }
}