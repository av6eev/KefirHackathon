using Dialogs.Specification.Panel;
using Presenter;
using ServerCore.Main.Commands;
using ServerCore.Main.Commands.Friends;
using ServerCore.Main.Commands.Party;
using ServerCore.Main.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Entities.Characters.Dialogs.Select.Actions
{
    public class CharacterSelectActionButtonPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly CharacterSelectActionsDialogModel _model;
        private readonly PanelButtonSpecification _buttonSpecification; 
        private readonly RectTransform _contentRoot;
        private readonly Button _buttonPrefab;
        private Button _view;
        
        public CharacterSelectActionButtonPresenter(IGameModel gameModel, CharacterSelectActionsDialogModel model, PanelButtonSpecification buttonSpecification, RectTransform contentRoot, Button buttonPrefab)
        {
            _gameModel = gameModel;
            _model = model;
            _buttonSpecification = buttonSpecification;
            _contentRoot = contentRoot;
            _buttonPrefab = buttonPrefab;
        }
        
        public void Init()
        {
            SetupButton();

            _view.onClick.AddListener(HandleClick);
        }

        public void Dispose()
        {
            _view.onClick.RemoveListener(HandleClick);
            
            Object.Destroy(_view.gameObject);
        }

        private void SetupButton()
        {
            _view = Object.Instantiate(_buttonPrefab, _contentRoot, true);
            var rectTransform = _view.GetComponent<RectTransform>();
            var text = _view.GetComponentInChildren<TextMeshProUGUI>();

            rectTransform.sizeDelta = new Vector2(_buttonSpecification.Width, _buttonSpecification.Height);
            rectTransform.localScale = Vector3.one;
            
            _view.GetComponent<Image>().color = _buttonSpecification.Color;

            switch (_buttonSpecification.ActionId)
            {
                case "party_invite":
                    if (_gameModel.PlayerModel.UserData.PartyData.Members.Collection.Count >= ServerConst.MaxPartyMemberCount)
                    {
                        _view.interactable = false;
                    }
                    break;
                case "friend_invite":
                    if (_gameModel.PlayerModel.UserData.FriendsData.Friends.Collection.Count >= ServerConst.MaxFriendsCount)
                    {
                        _view.interactable = false;
                    }
                    break;
            }
            
            text.color = _buttonSpecification.TextColor;
            text.text = _buttonSpecification.DescriptionText;
            text.font = _buttonSpecification.Font;
            text.fontSize = _buttonSpecification.FontSize;
        }

        private void HandleClick()
        {
            BaseCommand command = null;
            
            switch (_buttonSpecification.ActionId)
            {
                case "party_invite":
                    command = new InvitePartyCommand(_gameModel.PlayerModel.UserData.PlayerId.Value, _model.SelectedUserId);
                    Debug.Log("Send InvitePartyCommand");
                    break;
                case "friend_invite":
                    command = new InviteFriendCommand(_gameModel.PlayerModel.UserData.PlayerId.Value, _model.SelectedUserId);
                    Debug.Log("Send InviteFriendCommand");
                    break;
            }
            
            command?.Write(_gameModel.ServerConnectionModel.PlayerPeer);

            HandleClose();
        }
        
        private void HandleClose()
        {
            _model.IsOpened = false;
            _gameModel.DialogsCollection.Remove(_model);
        }
    }
}