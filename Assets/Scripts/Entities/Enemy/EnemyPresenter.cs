using System.Collections.Generic;
using Entities.Enemy.Collection;
using Entities.Enemy.State;
using Entities.Enemy.State.Behaviours;
using Presenter;
using UnityEngine;
using Updater;

namespace Entities.Enemy
{
    public class EnemyPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly EnemyModel _model;
        private readonly EnemiesCollectionView _collectionView;
        private readonly EnemyView _view;

        private readonly PresentersList _presenters = new();
        private readonly List<IUpdater> _updaters = new();

        public EnemyPresenter(IGameModel gameModel, EnemyModel model, EnemiesCollectionView collectionView, EnemyView view)
        {
            _gameModel = gameModel;
            _model = model;
            _collectionView = collectionView;
            _view = view;
        }
        
        public void Init()
        {
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
            
            Object.Destroy(_view.gameObject);

            foreach (var updater in _updaters)
            {
                _gameModel.UpdatersList.Remove(updater);
            }
            
            _updaters.Clear();
            
            _model.InTarget.OnChanged -= HandleInTarget;
        }

        private void HandleInTarget(bool newState, bool oldState)
        {
            _view.InTargetCircle.SetActive(newState);
        }
    }
}