using Entities.Characters.Dialogs.Party.Invite;
using Entities.Player;
using Entities.Player.Dialog.Party;
using Presenter;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Entities.Characters.Dialogs.Party
{
    public class CharacterBodyClickPresenter : IPresenter
    {
        private const string CharacterPartyInviteDialogSpecificationId = "party_invite_dialog";
        
        private readonly IGameModel _gameModel;
        private readonly CharacterModel _model;
        private readonly PlayerView _view;

        private CharacterPartyInviteDialogModel _inviteDialogModel;

        public CharacterBodyClickPresenter(IGameModel gameModel, CharacterModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _inviteDialogModel = new CharacterPartyInviteDialogModel(_gameModel.Specifications.DialogSpecifications[CharacterPartyInviteDialogSpecificationId]);
            
            _gameModel.InputModel.OnAttack += HandleClick;
        }

        public void Dispose()
        {
            _gameModel.InputModel.OnAttack -= HandleClick;
        }

        private void HandleClick()
        {
            if (Camera.main != null)
            {
                var ray = Camera.main.ScreenPointToRay(new Vector3(_gameModel.InputModel.MousePosition.x, _gameModel.InputModel.MousePosition.y, 0));
                
                if (UnityEngine.Physics.Raycast(ray, out var hit, 100f))
                {
                    if (hit.transform.CompareTag("Player") && !EventSystem.current.IsPointerOverGameObject())
                    {
                        _inviteDialogModel.InvitedUserId = _model.ServerData.PlayerId.Value;
                        
                        if (!_inviteDialogModel.IsOpened)
                        {
                            _gameModel.DialogsCollection.AddDialog(_inviteDialogModel);
                        }
                    }
                }
            }
        }
    }
}