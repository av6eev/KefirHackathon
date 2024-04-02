using Awaiter;
using Reactive.Field;

namespace Entities.Enemy.State.Behaviours
{
    public class EnemyIdleBehaviour : IBehaviour
    {
        public ReactiveField<bool> IsCompleted { get; } = new();
        
        private readonly EnemyView _view;
        private readonly CustomAwaiter _completeAwaiter;

        public EnemyIdleBehaviour(EnemyView view, CustomAwaiter completeAwaiter)
        {
            _view = view;
            _completeAwaiter = completeAwaiter;
        }

        public void Init()
        {
            _view.EnableIdleAnimation();
        }

        public void Dispose()
        {
            _view.DisableIdleAnimation();
            
            IsCompleted.Value = true;
            _completeAwaiter.Complete();
        }
    }
}