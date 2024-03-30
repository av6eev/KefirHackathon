using System.Collections.Generic;
using Awaiter;
using Presenter;

namespace Entities.Enemy.State.Behaviours
{
    public class ChangeBehaviourPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly EnemyModel _model;
        private readonly EnemyView _view;

        private IBehaviour _currentBehaviour;
        private CustomAwaiter _completeAwaiter = new();

        private readonly Dictionary<EnemyStateType, bool> _immediatelyForce = new()
        {
            { EnemyStateType.Idle, true },
            { EnemyStateType.Patrol, true },
            { EnemyStateType.MoveTowardsTarget, true },
            { EnemyStateType.Attack, true },
        };
        
        public ChangeBehaviourPresenter(IGameModel gameModel, EnemyModel model, EnemyView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _currentBehaviour = new EnemyIdleBehaviour(_view, _completeAwaiter);
            _currentBehaviour.Init();
            
            _model.CurrentState.OnChanged += HandleStateChanged;
        }

        public void Dispose()
        {
            _currentBehaviour.Dispose();
            
            _model.CurrentState.OnChanged -= HandleStateChanged;
        }

        private async void HandleStateChanged(EnemyStateType newState, EnemyStateType oldState)
        {
            if (!_currentBehaviour.IsCompleted.Value && _immediatelyForce[newState] == false)
            {
                await _completeAwaiter;
            }
            
            _currentBehaviour.Dispose();
            _completeAwaiter = new CustomAwaiter();
            
            switch (newState)
            {
                case EnemyStateType.Idle:
                    _currentBehaviour = new EnemyIdleBehaviour(_view, _completeAwaiter);
                    break;
                case EnemyStateType.Patrol:
                    _currentBehaviour = new EnemyPatrolBehaviour(_model, _view, _completeAwaiter, _gameModel.UpdatersList);
                    break;
                case EnemyStateType.MoveTowardsTarget:
                    _currentBehaviour = new EnemyMoveTowardsTargetBehaviour(_model, _view, _completeAwaiter, _gameModel.UpdatersList);
                    break;
                case EnemyStateType.Attack:
                    _currentBehaviour = new EnemyAttackBehaviour(_model, _view, _completeAwaiter, _gameModel.UpdatersList);
                    break;
            }
            
            _currentBehaviour.Init();
        }
    }
}