using UnityEngine;
using Updater;

namespace Entities.Enemy.State
{
    public class EnemyStateControlUpdater : IUpdater
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private float _currentPatrolTime;
        private float _currentIdleTime;
        
        public EnemyStateControlUpdater(EnemyModel model, EnemyView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Update(float deltaTime)
        {
            var distanceToPlayer = Vector3.Distance(_view.Position, _model.Target.Value.Position);

            if (_model.IsDied.Value && _model.CurrentState.Value == EnemyStateType.Death)
            {
                return;
            }
            else if (_model.IsDied.Value)
            {
                _model.CurrentState.Value = EnemyStateType.Death;
                return;
            }
            
            if (distanceToPlayer < _model.EnemySpecification.AttackRange)
            {
                _model.CurrentState.Value = EnemyStateType.Attack;
            }
            else if (distanceToPlayer < _model.EnemySpecification.ObserveRange)
            {
                _model.CurrentState.Value = EnemyStateType.MoveTowardsTarget;
            }
            else
            {
                if (_model.CurrentState.Value != EnemyStateType.Idle && _model.CurrentState.Value != EnemyStateType.Patrol)
                {
                    GetRandomNextState();
                }
                
                if (_model.CurrentState.Value == EnemyStateType.Idle)
                {
                    if (_currentIdleTime <= _model.EnemySpecification.IdleTime)
                    {
                        _currentIdleTime += deltaTime;
                    }
                    else
                    {
                        _currentIdleTime = 0;
                        GetRandomNextState();
                        return;
                    }
                    
                    return;
                }
                
                if (_model.CurrentState.Value == EnemyStateType.Patrol)
                {
                    var agent = _view.NavMeshAgent;
                    
                    if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
                    {
                        GetRandomNextState();                    
                    }
                }
            }
        }

        private void GetRandomNextState()
        {
            var random = Random.Range(0, 2);

            _model.CurrentState.Value = random == 0 ? EnemyStateType.Idle : EnemyStateType.Patrol;
        }
    }
}