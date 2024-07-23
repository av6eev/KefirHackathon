using Dialogs;
using Dialogs.Collection;
using Dialogs.Specification.Panel;
using Presenter;
using UnityEngine;

namespace Entities.Characters.Dialogs.Select.Actions
{
    public class CharacterSelectActionsDialogPresenter : DialogPresenter<CharacterSelectActionsDialogView>
    {
        private readonly IGameModel _gameModel;
        private readonly CharacterSelectActionsDialogModel _model;
        private readonly DialogsCollectionView _collectionView;
        
        private readonly PanelDialogSpecification _dialogSpecification;
        private readonly PresentersList _presenters = new();

        public CharacterSelectActionsDialogPresenter(IGameModel gameModel, CharacterSelectActionsDialogModel model, DialogsCollectionView collectionView) : base(gameModel, model, collectionView)
        {
            _gameModel = gameModel;
            _model = model;
            _collectionView = collectionView;
            _dialogSpecification = (PanelDialogSpecification)_model.Specification;
        }

        protected override void AfterInit()
        {
            InitializeButtons();
            Resize();
        }

        protected override void AfterDispose()
        {
            _presenters.Dispose();
            _presenters.Clear();
        }
        
        private void InitializeButtons()
        {
            foreach (var button in _dialogSpecification.Buttons)
            {
                _presenters.Add(new CharacterSelectActionButtonPresenter(_gameModel, _model, button, View.ContentRoot, _dialogSpecification.ButtonPrefab));
            }
            
            _presenters.Init();
        }

        private void Resize()
        {
            var height = 15f;
            var index = -1;

            for (var i = 0; i < _dialogSpecification.Buttons.Count; i++)
            {
                index++;
                height += 25;
            }

            height += index * 10;
            
            View.Root.sizeDelta = new Vector2(View.Root.sizeDelta.x, height);
        }
    }
}