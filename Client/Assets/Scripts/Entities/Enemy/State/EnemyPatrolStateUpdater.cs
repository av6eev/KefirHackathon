using UnityEngine;
using UnityEngine.AI;
using Updater;

namespace Entities.Enemy.State
{
    public class EnemyPatrolStateUpdater : IUpdater 
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;

        public EnemyPatrolStateUpdater(EnemyModel model, EnemyView view)
        {
            _model = model;
            _view = view;
        }

        public void Update(float deltaTime)
        {
            var agent = _view.NavMeshAgent;
            
            if (agent.hasPath && !(agent.remainingDistance <= agent.stoppingDistance))
            {
                return;
            }

            var point = GetRandomNavMeshPosition();
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            _view.NavMeshAgent.SetDestination(point);
        }
        
        private Vector3 GetRandomNavMeshPosition()
        {
            var center = _view.Position;
            
            for (var i = 0; i < 30; i++)
            {
                var randomPoint = center + Random.insideUnitSphere * 10;

                if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas))
                {
                    var newPosition = hit.position;
                    newPosition.y = _view.Position.y;
                    
                    return newPosition;
                }
            }

            return center;
        }
    }
}