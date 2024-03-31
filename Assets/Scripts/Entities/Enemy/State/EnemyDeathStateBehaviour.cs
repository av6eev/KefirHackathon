using Awaiter;
using Entities.Enemy.State.Behaviours;
using Reactive.Field;

namespace Entities.Enemy.State
{
    public class EnemyDeathStateBehaviour : IBehaviour
    {
        private readonly IGameModel _gameModel;
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly CustomAwaiter _completeAwaiter;
        public ReactiveField<bool> IsCompleted { get; }


        public EnemyDeathStateBehaviour(IGameModel gameModel, EnemyModel model, EnemyView view, CustomAwaiter completeAwaiter)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
            _completeAwaiter = completeAwaiter;
        }

        public void Init()
        {
            _view.EntityAnimationEvents.OnDeath += HandleDeath;
            
            _view.EntityAnimatorController.SetBool("IsMovement", false);
            _view.EntityAnimatorController.SetTrigger("IsDeath");
        }

        public void Dispose()
        {
            _view.EntityAnimationEvents.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            _gameModel.EnemiesCollection.Remove(_model);
        }
    }
}