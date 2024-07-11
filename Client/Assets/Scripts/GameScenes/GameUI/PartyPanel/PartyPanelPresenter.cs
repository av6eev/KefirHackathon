using GameScenes.GameUI.PartyPanel.Slot;
using Presenter;
using UnityEngine;

namespace GameScenes.GameUI.PartyPanel
{
    public class PartyPanelPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly PartyPanelModel _model;
        private readonly PartyPanelView _view;

        private readonly PresentersDictionary<string> _slotPresenters = new();
        
        public PartyPanelPresenter(GameModel gameModel, PartyPanelModel model, PartyPanelView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            HandlePartyStateChange();
            Resize();

            _gameModel.PlayerModel.UserData.PartyData.InParty.Changed += HandlePartyStateChange; 
            _gameModel.PlayerModel.UserData.PartyData.Members.OnAdd += HandleAddMember;
            _gameModel.PlayerModel.UserData.PartyData.Members.OnRemove += HandleRemoveMember;
            _gameModel.PlayerModel.UserData.PartyData.OwnerNickname.Changed += HandleOwnerChange;
        }

        public void Dispose()
        {
            _gameModel.PlayerModel.UserData.PartyData.InParty.Changed -= HandlePartyStateChange; 
            _gameModel.PlayerModel.UserData.PartyData.Members.OnAdd -= HandleAddMember;
            _gameModel.PlayerModel.UserData.PartyData.Members.OnRemove -= HandleRemoveMember;
            _gameModel.PlayerModel.UserData.PartyData.OwnerNickname.Changed -= HandleOwnerChange;
        }

        private void HandleOwnerChange()
        {
            foreach (var slotModel in _model.SlotModels)
            {
                slotModel.Value.IsOwner.Value = slotModel.Key == _gameModel.PlayerModel.UserData.PartyData.OwnerNickname.Value;
            }
        }
        
        private void HandleAddMember(string userName)
        {
            var model = new PartyPanelSlotModel(userName);
            var presenter = new PartyPanelSlotPresenter(_gameModel, model, _view.ContentRoot, _view.SlotPrefab);
            presenter.Init();
            
            _model.SlotModels.Add(userName, model);
            _slotPresenters.Add(userName, presenter);
            
            Resize();
        }

        private void HandleRemoveMember(string userName)
        {
            _model.SlotModels.Remove(userName);
            _slotPresenters.Remove(userName);
        }

        private void HandlePartyStateChange()
        {
            switch (_gameModel.PlayerModel.UserData.PartyData.InParty.Value)
            {
                case true:
                    _view.Root.gameObject.SetActive(true);
                    break;
                case false:
                    _view.Root.gameObject.SetActive(false);
                    break;
            }
        }

        private void Resize()
        {
            var height = 70f;
            var index = -1;

            for (var i = 0; i < _gameModel.PlayerModel.UserData.PartyData.Members.Collection.Count; i++)
            {
                index++;
                height += 30;
            }

            height += index * 10;
            
            _view.Root.sizeDelta = new Vector2(_view.Root.sizeDelta.x, height);
        }
    }
}