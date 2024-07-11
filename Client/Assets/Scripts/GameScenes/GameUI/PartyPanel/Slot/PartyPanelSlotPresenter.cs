using Presenter;
using UnityEngine;

namespace GameScenes.GameUI.PartyPanel.Slot
{
    public class PartyPanelSlotPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly PartyPanelSlotModel _model;
        private readonly PartyPanelSlotView _slotPrefab;
        private readonly RectTransform _contentRoot;

        private PartyPanelSlotView _view;

        public PartyPanelSlotPresenter(IGameModel gameModel, PartyPanelSlotModel model, RectTransform contentRoot, PartyPanelSlotView slotPrefab)
        {
            _gameModel = gameModel;
            _model = model;
            _contentRoot = contentRoot;
            _slotPrefab = slotPrefab;
        }
        
        public void Init()
        {
            _view = Object.Instantiate(_slotPrefab, _contentRoot);
            _view.PlayerNameText.text = _model.UserName;
            HandleOwnerStateChange(false, false);
            
            _model.IsOwner.OnChanged += HandleOwnerStateChange;
        }

        public void Dispose()
        {
            Object.Destroy(_view.gameObject);
            
            _model.IsOwner.OnChanged -= HandleOwnerStateChange;
        }

        private void HandleOwnerStateChange(bool newValue, bool oldValue)
        {
            _view.LeaderIcon.gameObject.SetActive(newValue);
        }
    }
}