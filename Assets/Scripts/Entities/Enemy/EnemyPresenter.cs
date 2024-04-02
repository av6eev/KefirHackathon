using System.Collections.Generic;
using Entities.Animation;
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
        private readonly Vector3 _spawnPosition;
        private EnemyView _view;

        private readonly PresentersList _presenters = new();
        private readonly List<IUpdater> _updaters = new();

        private ILoadObjectModel<GameObject> _loadObjectModel;

        public EnemyPresenter(IGameModel gameModel, EnemyModel model, GameObjectPull pull, Vector3 spawnPosition)
        {
            _gameModel = gameModel;
            _model = model;
            _pull = pull;
            _spawnPosition = spawnPosition;
        }
        
        public void Init()
        {
            _view = _pull.Get().GetComponent<EnemyView>();
            _view.transform.position = _spawnPosition;

            _view.NavMeshAgent.autoRepath = true;
            _view.NavMeshAgent.autoBraking = true;
            _view.NavMeshAgent.angularSpeed = _model.EnemySpecification.RotationSpeed;

            _presenters.Add(new EntityAnimationPresenter(_view.EntityAnimationEvents, _model.AnimationEvents));
            
            _updaters.Add(new EnemyStateControlUpdater(_model, _view));
            _updaters.Add(new EnemyInfoUpdater(_model, _view));

            foreach (var updater in _updaters)
            {
                _gameModel.UpdatersList.Add(updater);
            }
            
            _presenters.Add(new EnemyHealthPresenter(_gameModel, _model, _view));
            _presenters.Add(new ChangeBehaviourPresenter(_gameModel, _model, _view));
            _presenters.Init();

            _model.InTarget.OnChanged += HandleInTarget;
            _model.IsDied.OnChanged += HandleDie;
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
            _model.IsDied.OnChanged -= HandleDie;
        }

        private void HandleDie(bool newValue, bool oldValue)
        {
            if (newValue)
            {
                _gameModel.PlayerModel.KillCount.Value++;
            }
        }

        private void HandleInTarget(bool newState, bool oldState)
        {
            _view.InTargetCircle.SetActive(newState);
        }
    }
}