using DeBuff;
using DeBuff.Collection;
using Presenter;
using UnityEngine;

namespace GameScenes.GameUI.DeBuffPanel
{
    public class DeBuffPanelPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly DeBuffsCollection _model;
        private readonly DeBuffPanelView _view;

        private readonly PresentersDictionary<DeBuffModel> _presenters = new();
        
        public DeBuffPanelPresenter(IGameModel gameModel, DeBuffsCollection model, DeBuffPanelView view)
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
            
            Resize();
            
            _model.AddEvent.OnChanged += HandleAdd;
            _model.RemoveEvent.OnChanged += HandleRemove;
        }

        public void Dispose()
        {
            _model.AddEvent.OnChanged -= HandleAdd;
            _model.RemoveEvent.OnChanged -= HandleRemove;
        }

        private void HandleAdd(DeBuffModel model)
        {
            var view = Object.Instantiate(_view.DeBuffPrefab, _view.ContentRoot);
            var presenter = new DeBuffPresenter(_gameModel, model, view);
            
            Resize();
            
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        private void HandleRemove(DeBuffModel model)
        {
            Resize();
            
            _presenters.Remove(model);
        }

        private void Resize()
        {
            if (_model.IsEmpty)
            {
                _view.Root.sizeDelta = new Vector2(_view.Root.sizeDelta.x, 70);
                return;
            }
            
            var height = 70f;
            var index = -1;
            
            foreach (var model in _model.GetModels())
            {
                index++;
                height -= 35;
            }

            height += index * 10;
            
            _view.Root.sizeDelta = new Vector2(_view.Root.sizeDelta.x, height);
        }
    }
}