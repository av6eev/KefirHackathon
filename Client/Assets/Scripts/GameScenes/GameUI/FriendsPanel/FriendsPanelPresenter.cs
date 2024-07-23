using System.Linq;
using GameScenes.GameUI.FriendsPanel.Slot;
using Presenter;
using UnityEngine;

namespace GameScenes.GameUI.FriendsPanel
{
    public class FriendsPanelPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly FriendsPanelModel _model;
        private readonly FriendsPanelView _view;

        private readonly PresentersDictionary<string> _slotPresenters = new();
        
        public FriendsPanelPresenter(IGameModel gameModel, FriendsPanelModel model, FriendsPanelView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _view.Root.gameObject.SetActive(false);
            
            _gameModel.PlayerModel.UserData.FriendsData.Friends.OnAdd += HandleAddMember;
            _gameModel.PlayerModel.UserData.FriendsData.Friends.OnRemove += HandleRemoveMember;

            _gameModel.PlayerModel.UserData.FriendsData.OnlineFriends.OnAdd += HandleFriendConnect;
            _gameModel.PlayerModel.UserData.FriendsData.OnlineFriends.OnRemove += HandleFriendDisconnect;

            _gameModel.InputModel.OnFriendsPanelToggle += HandleStateChange;
        }

        public void Dispose()
        {
            _gameModel.PlayerModel.UserData.FriendsData.Friends.OnAdd -= HandleAddMember;
            _gameModel.PlayerModel.UserData.FriendsData.Friends.OnRemove -= HandleRemoveMember;
            
            _gameModel.PlayerModel.UserData.FriendsData.OnlineFriends.OnAdd -= HandleFriendConnect;
            _gameModel.PlayerModel.UserData.FriendsData.OnlineFriends.OnRemove -= HandleFriendDisconnect;
            
            _gameModel.InputModel.OnFriendsPanelToggle -= HandleStateChange;
        }

        private void HandleFriendConnect(string nickname)
        {
            if (!_model.IsOpen) return;

            if (_model.SlotModels.TryGetValue(nickname, out var slotModel))
            {
                slotModel.IsOnline.Value = true;
            }
        }

        private void HandleFriendDisconnect(string nickname)
        {
            if (!_model.IsOpen) return;
            
            if (_model.SlotModels.TryGetValue(nickname, out var slotModel))
            {
                slotModel.IsOnline.Value = false;
            }
        }

        private void HandleAddMember(string nickname)
        {
            if (!_model.IsOpen) return;

            var isOnline = _gameModel.PlayerModel.UserData.FriendsData.OnlineFriends.Contains(nickname);
            var model = new FriendsPanelSlotModel(nickname, isOnline);
            var presenter = new FriendsPanelSlotPresenter(_gameModel, model, _view.ContentRoot, _view.SlotPrefab);
            presenter.Init();
            
            _model.SlotModels.Add(nickname, model);
            _slotPresenters.Add(nickname, presenter);
            
            Resize();
        }

        private void HandleRemoveMember(string userName)
        {
            _model.SlotModels.Remove(userName);
            _slotPresenters.Remove(userName);
        }

        private void HandleStateChange()
        {
            _model.IsOpen = !_model.IsOpen;
            
            switch (_model.IsOpen)
            {
                case true:
                    foreach (var nickname in _gameModel.PlayerModel.UserData.FriendsData.Friends.Collection)
                    {
                        HandleAddMember(nickname);
                    }
                    
                    Resize();
                    _view.Root.gameObject.SetActive(true);
                    break;
                case false:
                    _view.Root.gameObject.SetActive(false);
                    
                    _slotPresenters.Dispose();
                    _slotPresenters.Clear();
                    
                    _model.SlotModels.Clear();
                    break;
            }
        }

        private void Resize()
        {
            var friendsCount = _gameModel.PlayerModel.UserData.FriendsData.Friends.Collection.Count;

            if (friendsCount == 0) return;
            
            var height = 70f;
            var index = -1;

            for (var i = 0; i < friendsCount; i++)
            {
                index++;
                height += 30;
            }

            height += index * 10;
            
            _view.Root.sizeDelta = new Vector2(_view.Root.sizeDelta.x, height);
        }
    }
}