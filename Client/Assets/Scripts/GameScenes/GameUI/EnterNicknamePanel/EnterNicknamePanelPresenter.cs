using Presenter;
using UnityEngine;

namespace GameScenes.GameUI.EnterNicknamePanel
{
    public class EnterNicknamePanelPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly EnterNicknamePanelModel _model;
        private readonly EnterNicknamePanelView _view;

        private string _tempInputNickname;
        
        public EnterNicknamePanelPresenter(IGameModel gameModel, EnterNicknamePanelModel model, EnterNicknamePanelView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }

        public void Init()
        {
            HandleStateChange(_model.IsShown.Value, false);
            _model.IsShown.OnChanged += HandleStateChange;
            
            _view.InputField.onValueChanged.AddListener(HandleValueChange);
            _view.ConfirmButton.onClick.AddListener(HandleConfirm);
        }

        public void Dispose()
        {
            _model.IsShown.OnChanged -= HandleStateChange;

            _view.InputField.onValueChanged.RemoveListener(HandleValueChange);
            _view.ConfirmButton.onClick.RemoveListener(HandleConfirm);
        }

        private void HandleStateChange(bool newValue, bool oldValue)
        {
            _view.gameObject.SetActive(newValue);
        }

        private void HandleConfirm()
        {
            _model.ConfirmNickname(_tempInputNickname);
            _view.gameObject.SetActive(false);
        }

        private void HandleValueChange(string newValue)
        {
            _tempInputNickname = newValue;
            Debug.Log(_tempInputNickname);
        }
    }
}