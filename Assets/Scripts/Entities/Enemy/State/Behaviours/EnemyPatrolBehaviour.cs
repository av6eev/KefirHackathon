using Awaiter;
using Reactive.Field;
using Updater;

namespace Entities.Enemy.State.Behaviours
{
    public class EnemyPatrolBehaviour : IBehaviour
    {
        public ReactiveField<bool> IsCompleted { get; } = new();
        
        private readonly EnemyModel _enemyModel;
        private readonly EnemyView _view;
        
        private readonly CustomAwaiter _completeAwaiter;
        private readonly IUpdatersList _updatersList;
        private EnemyPatrolStateUpdater _updater;

        public EnemyPatrolBehaviour(EnemyModel enemyModel, EnemyView view, CustomAwaiter completeAwaiter, IUpdatersList updatersList)
        {
            _enemyModel = enemyModel;
            _view = view;
            _completeAwaiter = completeAwaiter;
            _updatersList = updatersList;
        }

        public void Init()
        {
            var specification = _enemyModel.EnemySpecification;
            
            _enemyModel.CurrentSpeed.Value = specification.PatrolSpeed;
            _view.NavMeshAgent.speed = specification.PatrolSpeed;
            
            _view.SetAnimationMovementSpeed(0.49f);
            
            _updater = new EnemyPatrolStateUpdater(_enemyModel, _view);
            _updatersList.Add(_updater);
        }

        public void Dispose()
        {
            _view.NavMeshAgent.speed = 0;
            _view.NavMeshAgent.ResetPath();
            
            _view.SetAnimationMovementSpeed(0);
            
            _updatersList.Remove(_updater);
            
            IsCompleted.Value = true;
            _completeAwaiter.Complete();
        }
    }
}