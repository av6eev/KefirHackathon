using Awaiter;
using Reactive.Field;
using Updater;

namespace Entities.Enemy.State.Behaviours
{
    public class EnemyMoveTowardsTargetBehaviour : IBehaviour
    {
        public ReactiveField<bool> IsCompleted { get; } = new();
        
        private readonly EnemyModel _enemyModel;
        private readonly EnemyView _view;
        
        private readonly CustomAwaiter _completeAwaiter;
        private readonly IUpdatersList _updatersList;
        private EnemyMoveTowardsTargetStateUpdater _updater;

        public EnemyMoveTowardsTargetBehaviour(EnemyModel enemyModel, EnemyView view, CustomAwaiter completeAwaiter, IUpdatersList updatersList)
        {
            _enemyModel = enemyModel;
            _view = view;
            _completeAwaiter = completeAwaiter;
            _updatersList = updatersList;
        }

        public void Init()
        {
            var specification = _enemyModel.EnemySpecification;
            
            _enemyModel.CurrentSpeed.Value = specification.MoveTowardsTargetSpeed;
            _view.NavMeshAgent.speed = specification.MoveTowardsTargetSpeed;
            
            _view.SetAnimationMovementSpeed(1f);
            
            _updater = new EnemyMoveTowardsTargetStateUpdater(_enemyModel, _view);
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