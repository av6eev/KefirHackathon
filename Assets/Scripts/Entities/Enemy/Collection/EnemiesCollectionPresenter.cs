using System.Collections.Generic;
using Loader.Object;
using Presenter;
using UnityEngine;
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
        
        public EnemiesCollectionPresenter(IGameModel gameModel, EnemiesCollection model, EnemiesCollectionView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public async void Init()
        {
            var loadObjectModel = _gameModel.LoadObjectsModel.Load<GameObject>(EnemyVariants.Wizard);
            await loadObjectModel.LoadAwaiter;

            _view.EnemyPull.Add(EnemyVariants.Wizard, new GameObjectPull(loadObjectModel.Result, _view.Root, 10));
            
            _loadObjectModels.Add(loadObjectModel);
            
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
            var presenter = new EnemyPresenter(_gameModel, model, _view.EnemyPull[model.EnemySpecification.PrefabId]);
            
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        private void HandleRemove(EnemyModel model)
        {
            _presenters.Remove(model);
        }
    }
}