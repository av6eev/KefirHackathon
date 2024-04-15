using System.Collections.Generic;
using Loader.Object;
using Presenter;
using UnityEngine;
using UnityEngine.AI;
using Utilities.Pull;

namespace Entities.Enemy.Collection
{
    public class EnemiesCollectionPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly EnemiesCollection _model;
        private readonly EnemiesCollectionView _view;

        private readonly PresentersDictionary<EnemyModel> _presenters = new();
        private List<ILoadObjectModel<GameObject>> _loadObjectModels = new();
        private readonly Dictionary<int, int> _enemiesAtPointsCounter = new();
        
        public EnemiesCollectionPresenter(IGameModel gameModel, EnemiesCollection model, EnemiesCollectionView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public async void Init()
        {
            var mageOneObjectModel = _gameModel.LoadObjectsModel.Load<GameObject>(EnemyVariants.MageOne);
            await mageOneObjectModel.LoadAwaiter;

            _view.EnemyPull.Add(EnemyVariants.MageOne, new GameObjectPull(mageOneObjectModel.Result, _view.Root, 10));
            _loadObjectModels.Add(mageOneObjectModel);
            
            var mageTwoObjectModel = _gameModel.LoadObjectsModel.Load<GameObject>(EnemyVariants.MageTwo);
            await mageTwoObjectModel.LoadAwaiter;

            _view.EnemyPull.Add(EnemyVariants.MageTwo, new GameObjectPull(mageTwoObjectModel.Result, _view.Root, 10));
            _loadObjectModels.Add(mageTwoObjectModel);
            
            foreach (var model in _model.GetModels())
            {
                HandleAdd(model);
            }

            _model.AddEvent.OnChanged += HandleAdd;
            _model.RemoveEvent.OnChanged += HandleRemove;
        }

        public void Dispose()
        {
            _presenters.Dispose();
            _presenters.Clear();

            foreach (var pull in _view.EnemyPull.Values)
            {
                pull.Dispose();
            }
            
            foreach (var loadObjectModel in _loadObjectModels)
            {
                _gameModel.LoadObjectsModel.Unload(loadObjectModel);
            }
            
            _model.AddEvent.OnChanged -= HandleAdd;
            _model.RemoveEvent.OnChanged -= HandleRemove;
        }

        private void HandleAdd(EnemyModel model)
        {
            var presenter = new EnemyPresenter(_gameModel, model, _view.EnemyPull[model.EnemySpecification.PrefabId], GetRandomNavMeshPosition());
            
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        private void HandleRemove(EnemyModel model)
        {
            _presenters.Remove(model);
            
            Object.Instantiate(model.EnemySpecification.CastGameObjectPrefabId, model.Position, Quaternion.identity);
        }
        
        private Vector3 GetRandomNavMeshPosition()
        {
            var randomIndex = Random.Range(0, _view.EnemySpawnPositions.Count);
            var centerPosition = _view.EnemySpawnPositions[randomIndex].position;

            if (_enemiesAtPointsCounter.TryGetValue(randomIndex, out var value))
            {
                if (value >= 3)
                {
                    GetRandomNavMeshPosition();
                }
            }
            
            if (!_enemiesAtPointsCounter.TryAdd(randomIndex, 1))
            {
                _enemiesAtPointsCounter[randomIndex]++;
            }

            for (var i = 0; i < 15; i++)
            {
                var randomPoint = centerPosition + Random.insideUnitSphere * 10;

                if (NavMesh.SamplePosition(randomPoint, out var hit, 5.0f, NavMesh.AllAreas))
                {
                    var newPosition = hit.position;
                    
                    return newPosition;
                }
            }

            return centerPosition;
        }
    }
}