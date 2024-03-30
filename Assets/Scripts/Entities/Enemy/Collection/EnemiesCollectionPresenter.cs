using Loader.Object;
using Presenter;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy.Collection
{
    public class EnemiesCollectionPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly EnemiesCollection _model;
        private readonly EnemiesCollectionView _view;

        private readonly PresentersDictionary<EnemyModel> _presenters = new();
        private ILoadObjectModel<GameObject> _loadObjectModel;

        public EnemiesCollectionPresenter(IGameModel gameModel, EnemiesCollection model, EnemiesCollectionView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
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
            
            _model.AddEvent.OnChanged -= HandleAdd;
            _model.RemoveEvent.OnChanged -= HandleRemove;
        }

        private async void HandleAdd(EnemyModel model)
        {
            _loadObjectModel = _gameModel.LoadObjectsModel.Load<GameObject>(model.EnemySpecification.PrefabId);
            await _loadObjectModel.LoadAwaiter;

            var component = _loadObjectModel.Result.GetComponent<EnemyView>();
            var view = Object.Instantiate(component, GetRandomNavMeshPosition(component), Quaternion.identity, _view.Root);
            var presenter = new EnemyPresenter(_gameModel, model, _view, view);
            
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        private void HandleRemove(EnemyModel model)
        {
            _gameModel.LoadObjectsModel.Unload(_loadObjectModel);
            
            _presenters.Remove(model);
        }

        private Vector3 GetRandomNavMeshPosition(EnemyView component)
        {
            while (true)
            {
                var randomPoint = _view.SpawnZoneCenter.position + Random.insideUnitSphere * 10;

                if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas))
                {
                    var newPosition = hit.position;
                    newPosition.y = component.Position.y;
                    
                    return newPosition;
                }
            }
        }
    }
}