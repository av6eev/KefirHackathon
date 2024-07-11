using Entities.Player.Dialog.Party;
using Presenter;
using ServerCore.Main.Party;

namespace Entities.Player
{
    public class PlayerPartyRequestPresenter : IPresenter
    {
        private const string CharacterPartyDecisionDialogSpecificationId = "party_decision_dialog";
        
        private readonly GameModel _gameModel;
        private readonly PlayerModel _model;
        
        private PlayerPartyDecisionDialogModel _decisionDialogModel;

        public PlayerPartyRequestPresenter(GameModel gameModel, PlayerModel model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            _decisionDialogModel = new PlayerPartyDecisionDialogModel(_gameModel.Specifications.DialogSpecifications[CharacterPartyDecisionDialogSpecificationId]);
            
            _model.UserData.Invites.OnAdd += HandlePartyInvite;
        }

        public void Dispose()
        {
            _model.UserData.Invites.OnAdd -= HandlePartyInvite;
        }

        private void HandlePartyInvite(PartyInviteData inviteData)
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