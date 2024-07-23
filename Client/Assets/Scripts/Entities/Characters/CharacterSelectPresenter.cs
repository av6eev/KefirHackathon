using Dialogs.Specification.Panel;
using Entities.Characters.Dialogs.Panel;
using Entities.Player;
using Presenter;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Entities.Characters
{
    public class CharacterSelectPresenter : IPresenter
    {
        private const string CharacterSelectDialogSpecificationId = "character_select_dialog";
        
        private readonly IGameModel _gameModel;
        private readonly CharacterModel _model;
        private readonly PlayerView _view;

        private CharacterSelectDialogModel _dialogModel;

        public CharacterSelectPresenter(IGameModel gameModel, CharacterModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            HandleSelect(false, false);
            
            _dialogModel = new CharacterSelectDialogModel(_gameModel.Specifications.DialogSpecifications[CharacterSelectDialogSpecificationId]);

            _model.IsSelected.OnChanged += HandleSelect;
            _gameModel.InputModel.OnAttack += HandleClick;
        }

        public void Dispose()
        {
            _model.IsSelected.OnChanged -= HandleSelect;
            _gameModel.InputModel.OnAttack -= HandleClick;
        }

        private void HandleSelect(bool newValue, bool oldValue)
        {
            switch (newValue)
            {
                case true:
                    _view.SelectedToolTip.gameObject.SetActive(true);
                    break;
                case false:
                    _view.SelectedToolTip.gameObject.SetActive(false);
                    break;
            }
        }

        private void HandleClick()
        {
            if (Camera.main != null)
            {
                var ray = Camera.main.ScreenPointToRay(new Vector3(_gameModel.InputModel.MousePosition.x, _gameModel.InputModel.MousePosition.y, 0));
                
                if (UnityEngine.Physics.Raycast(ray, out var hit, 100f))
                {
                    if (hit.transform.CompareTag("Player") && !EventSystem.current.IsPointerOverGameObject())
                    {
                        _model.IsSelected.Value = true;
                        
                        _dialogModel.SelectedUserId = _model.ServerData.PlayerId.Value;
                        
                        if (!_dialogModel.IsOpened)
                        {
                            _gameModel.DialogsCollection.AddDialog(_dialogModel);
                        }
                    }
                }
            }
        }
    }
}