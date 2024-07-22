using Dialogs;
using Dialogs.Collection;
using ServerCore.Main.Commands.Friends;
using UnityEngine;

namespace Entities.Player.Dialog.Friends
{
    public class PlayerFriendDecisionDialogPresenter : DialogPresenter<PlayerFriendDecisionDialogView>
    {
        private readonly IGameModel _gameModel;
        private readonly PlayerFriendDecisionDialogModel _model;
        private readonly DialogsCollectionView _collectionView;

        public PlayerFriendDecisionDialogPresenter(IGameModel gameModel, PlayerFriendDecisionDialogModel model, DialogsCollectionView collectionView) : base(gameModel, model, collectionView)
        {
            _gameModel = gameModel;
            _model = model;
            _collectionView = collectionView;
        }

        protected override void AfterInit()
        {
            View.InviteFromUserNameText.text = _model.OwnerNickname;
            
            View.AcceptButton.onClick.AddListener(HandleAccept);
            View.DeclineButton.onClick.AddListener(HandleDecline);
        }

        protected override void AfterDispose()
        {
            View.AcceptButton.onClick.RemoveListener(HandleAccept);
            View.DeclineButton.onClick.RemoveListener(HandleDecline);
        }

        private void HandleAccept()
        {
            var command = new AcceptFriendCommand(_gameModel.PlayerModel.UserData.PlayerId.Value, _model.InviteId);
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
            Debug.Log("Send AcceptFriendCommand");
            HandleClose();
        }

        private void HandleDecline()
        {
            var command = new DeclineFriendCommand(_gameModel.PlayerModel.UserData.PlayerId.Value, _model.InviteId);
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
            Debug.Log("Send DeclineFriendCommand");
            HandleClose();
        }
    }
}