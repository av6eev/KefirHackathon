using Presenter;
using Quest;
using Quest.Collection;
using UnityEngine;

namespace GameScenes.GameUI.QuestPanel
{
    public class QuestPanelPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly QuestsCollection _model;
        private readonly QuestPanelView _view;

        private readonly PresentersDictionary<QuestModel> _presenters = new();
        
        public QuestPanelPresenter(IGameModel gameModel, QuestsCollection model, QuestPanelView view)
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
            
            _view.NoActiveQuests.SetActive(_model.IsEmpty);

            _model.AddEvent.OnChanged += HandleAdd;
            _model.RemoveEvent.OnChanged += HandleRemove;
        }

        public void Dispose()
        {
            _model.AddEvent.OnChanged -= HandleAdd;
            _model.RemoveEvent.OnChanged -= HandleRemove;
        }

        private void HandleAdd(QuestModel model)
        {
            var view = Object.Instantiate(_view.QuestPrefab, _view.ContentRoot);
            var presenter = new QuestPresenter(_gameModel, model, view);
            
            _view.NoActiveQuests.SetActive(_model.IsEmpty);
            Resize();
            
            presenter.Init();
            _presenters.Add(model, presenter);
        }

        private void HandleRemove(QuestModel model)
        {
            _view.NoActiveQuests.SetActive(_model.IsEmpty);
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
                height += 60;
            }

            height += index * 10;
            
            _view.Root.sizeDelta = new Vector2(_view.Root.sizeDelta.x, height);
        }
    }
}