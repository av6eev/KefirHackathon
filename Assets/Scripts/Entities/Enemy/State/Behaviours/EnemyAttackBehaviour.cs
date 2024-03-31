using Awaiter;
using Reactive.Field;
using UnityEngine;
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
        private FireBallView _castedGo;

        public EnemyAttackBehaviour(EnemyModel enemyModel, EnemyView view, CustomAwaiter completeAwaiter, IUpdatersList updatersList)
        {
            _enemyModel = enemyModel;
            _view = view;
            _completeAwaiter = completeAwaiter;
            _updatersList = updatersList;
        }

        public void Init()
        {
            _view.EntityAnimatorController.SetBool("IsMovement", false);
            _view.EntityAnimatorController.SetTrigger("Attack");
            
            _castedGo = Object.Instantiate(_enemyModel.EnemySpecification.CastGameObjectPrefabId, _view.Position + _view.Forward, Quaternion.identity);
            _castedGo.transform.SetParent(_view.transform);
            _castedGo.Direction = _view.Forward;
        }

        public void Dispose()
        {
            _enemyModel.IsAttack.Value = false;
            IsCompleted.Value = true;

            _completeAwaiter.Complete();
        }
    }
}