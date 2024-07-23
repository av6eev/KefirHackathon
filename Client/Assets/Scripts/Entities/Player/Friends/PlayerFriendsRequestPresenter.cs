using Entities.Player.Dialog.Friends;
using Presenter;
using ServerCore.Main.Friends;

namespace Entities.Player.Friends
{
    public class PlayerFriendsRequestPresenter : IPresenter
    {
        private const string CharacterFriendDecisionDialogSpecificationId = "friends_decision_dialog";
        
        private readonly GameModel _gameModel;
        private readonly PlayerModel _model;
        
        private PlayerFriendDecisionDialogModel _decisionDialogModel;

        public PlayerFriendsRequestPresenter(GameModel gameModel, PlayerModel model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            _decisionDialogModel = new PlayerFriendDecisionDialogModel(_gameModel.Specifications.DialogSpecifications[CharacterFriendDecisionDialogSpecificationId]);
            
            _model.UserData.FriendInvites.OnAdd += HandleFriendInvite;
        }

        public void Dispose()
        {
            _model.UserData.FriendInvites.OnAdd -= HandleFriendInvite;
        }

        private void HandleFriendInvite(FriendInviteData inviteData)
        {
            //TODO: queue
            
            if (!_decisionDialogModel.IsOpened)
            {
                _decisionDialogModel.OwnerNickname = inviteData.InviteFromUserNickname.Value;
                _decisionDialogModel.InviteId = inviteData.InviteId.Value;
                
                _gameModel.DialogsCollection.AddDialog(_decisionDialogModel);
            }
        }
    }
}