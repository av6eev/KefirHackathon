using Dialogs;
using Dialogs.Collection;
using ServerCore.Main.Commands.Party;
using UnityEngine;

namespace Entities.Characters.Dialogs.Party.Invite
{
    public class CharacterPartyInviteDialogPresenter : DialogPresenter<CharacterPartyInviteDialogView>
    {
        private readonly IGameModel _gameModel;
        private readonly CharacterPartyInviteDialogModel _model;
        private readonly DialogsCollectionView _collectionView;

        public CharacterPartyInviteDialogPresenter(IGameModel gameModel, CharacterPartyInviteDialogModel model, DialogsCollectionView collectionView) : base(gameModel, model, collectionView)
        {
            _gameModel = gameModel;
            _model = model;
            _collectionView = collectionView;
        }

        protected override void AfterInit()
        {
            View.InvitedUserNameText.text = _gameModel.CharactersCollection.GetModel(_model.InvitedUserId).ServerData.PlayerNickname.Value;
            
            View.InviteButton.onClick.AddListener(HandleClick);
        }

        protected override void AfterDispose()
        {
            View.InviteButton.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            var command = new InvitePartyCommand(_gameModel.PlayerModel.UserData.PlayerId.Value, _model.InvitedUserId);
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
            Debug.Log("Send InvitePartyCommand");
            HandleClose();
        }
    }
}