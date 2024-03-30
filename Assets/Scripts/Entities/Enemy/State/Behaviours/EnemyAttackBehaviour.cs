using Awaiter;
using Reactive.Field;
using Updater;

namespace Entities.Enemy.State.Behaviours
{
    public class EnemyAttackBehaviour : IBehaviour
    {
        public ReactiveField<bool> IsCompleted { get; } = new();
        
        private readonly EnemyModel _enemyModel;
        private readonly EnemyView _view;
        
        private readonly CustomAwaiter _completeAwaiter;
        private readonly IUpdatersList _updatersList;
        private EnemyAttackStateUpdater _updater;

        public EnemyAttackBehaviour(EnemyModel enemyModel, EnemyView view, CustomAwaiter completeAwaiter, IUpdatersList updatersList)
        {
            _enemyModel = enemyModel;
            _view = view;
            _completeAwaiter = completeAwaiter;
            _updatersList = updatersList;
        }

        public void Init()
        {
            _updater = new EnemyAttackStateUpdater(_enemyModel, _view);
            _updatersList.Add(_updater);
        }

        public void Dispose()
        {
            _updatersList.Remove(_updater);
            
            IsCompleted.Value = true;
            _completeAwaiter.Complete();
        }
    }
}