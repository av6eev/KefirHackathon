using System.Collections.Generic;
using Entities.Enemy.State;
using Entities.Enemy.State.Behaviours;
using Loader.Object;
using Presenter;
using UnityEngine;
using UnityEngine.AI;
using Updater;
using Utilities.Pull;

namespace Entities.Enemy
{
    public class EnemyPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly EnemyModel _model;
        
        private GameObjectPull _pull;
        private EnemyView _view;

        private readonly PresentersList _presenters = new();
        private readonly List<IUpdater> _updaters = new();

        private ILoadObjectModel<GameObject> _loadObjectModel;

        public EnemyPresenter(IGameModel gameModel, EnemyModel model, GameObjectPull pull)
        {
            _gameModel = gameModel;
            _model = model;
            _pull = pull;
        }
        
        public void Init()
        {
            _view = _pull.Get().GetComponent<EnemyView>();
            _view.transform.position = GetRandomNavMeshPosition();

            _view.NavMeshAgent.autoRepath = true;
            _view.NavMeshAgent.autoBraking = true;
            _view.NavMeshAgent.angularSpeed = _model.EnemySpecification.RotationSpeed;

            _updaters.Add(new EnemyStateControlUpdater(_model, _view));
            _updaters.Add(new EnemyInfoUpdater(_model, _view));

            foreach (var updater in _updaters)
            {
                _gameModel.UpdatersList.Add(updater);
            }
            
            // _presenters.Add(new EnemyHealthPresenter(_gameModel, _model, _view));
            _presenters.Add(new ChangeBehaviourPresenter(_gameModel, _model, _view));
            _presenters.Init();

            _model.InTarget.OnChanged += HandleInTarget;
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();
            
            foreach (var updater in _updaters)
            {
                _gameModel.UpdatersList.Remove(updater);
            }
            
            _updaters.Clear();
            
            _pull.Put(_view.gameObject);
            
            _model.InTarget.OnChanged -= HandleInTarget;
        }
        
        private void HandleInTarget(bool newState, bool oldState)
        {
            _view.InTargetCircle.SetActive(newState);
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