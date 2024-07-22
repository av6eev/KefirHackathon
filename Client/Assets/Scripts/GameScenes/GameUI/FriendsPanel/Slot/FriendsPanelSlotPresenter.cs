using Presenter;
using UnityEngine;

namespace GameScenes.GameUI.FriendsPanel.Slot
{
    public class FriendsPanelSlotPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly FriendsPanelSlotModel _model;
        private readonly RectTransform _contentRoot;
        private readonly FriendsPanelSlotView _slotPrefab;
        
        private FriendsPanelSlotView _view;

        public FriendsPanelSlotPresenter(IGameModel gameModel, FriendsPanelSlotModel model, RectTransform contentRoot, FriendsPanelSlotView slotPrefab)
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
            HandleStatusChange(false, false);
            
            _model.IsOnline.OnChanged += HandleStatusChange;
        }

        public void Dispose()
        {
            Object.Destroy(_view.gameObject);
            
            _model.IsOnline.OnChanged -= HandleStatusChange;
        }

        private void HandleStatusChange(bool newValue, bool oldValue)
        {
            _view.StatusIcon.color = newValue switch
            {
                true => Color.green,
                false => Color.gray
            };
        }
    }
}