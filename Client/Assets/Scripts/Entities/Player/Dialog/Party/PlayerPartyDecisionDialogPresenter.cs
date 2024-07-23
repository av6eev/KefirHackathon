using Dialogs;
using Dialogs.Collection;
using ServerCore.Main.Commands.Party;
using UnityEngine;

namespace Entities.Player.Dialog.Party
{
    public class PlayerPartyDecisionDialogPresenter : DialogPresenter<PlayerPartyDecisionDialogView>
    {
        private readonly IGameModel _gameModel;
        private readonly PlayerPartyDecisionDialogModel _model;
        private readonly DialogsCollectionView _collectionView;

        public PlayerPartyDecisionDialogPresenter(IGameModel gameModel, PlayerPartyDecisionDialogModel model, DialogsCollectionView collectionView) : base(gameModel, model, collectionView)
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
            var command = new AcceptPartyCommand(_gameModel.PlayerModel.UserData.PlayerId.Value, _model.InviteId);
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
            Debug.Log("Send AcceptPartyCommand");
            HandleClose();
        }

        private void HandleDecline()
        {
            var command = new DeclinePartyCommand(_gameModel.PlayerModel.UserData.PlayerId.Value, _model.InviteId);
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
            Debug.Log("Send DeclinePartyCommand");
            HandleClose();
        }
    }
}