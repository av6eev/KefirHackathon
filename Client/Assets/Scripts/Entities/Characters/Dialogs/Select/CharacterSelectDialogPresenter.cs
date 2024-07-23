using Dialogs;
using Dialogs.Collection;
using Dialogs.Specification.Panel;
using Entities.Characters.Dialogs.Select.Actions;
using UnityEngine.UI;

namespace Entities.Characters.Dialogs.Panel
{
    public class CharacterSelectDialogPresenter : DialogPresenter<CharacterSelectDialogView>
    {
        private const string CharacterSelectActionsDialogSpecificationId = "character_select_actions_dialog";
        
        private readonly IGameModel _gameModel;
        private readonly CharacterSelectDialogModel _model;
        private readonly DialogsCollectionView _collectionView;
        
        private CharacterSelectActionsDialogModel _dialogModel;

        public CharacterSelectDialogPresenter(IGameModel gameModel, CharacterSelectDialogModel model, DialogsCollectionView collectionView) : base(gameModel, model, collectionView)
        {
            _gameModel = gameModel;
            _model = model;
            _collectionView = collectionView;
        }

        protected override void AfterInit()
        {
            View.NicknameText.text = _gameModel.CharactersCollection.GetModel(_model.SelectedUserId).ServerData.PlayerNickname.Value;
            
            _dialogModel = new CharacterSelectActionsDialogModel((PanelDialogSpecification)_gameModel.Specifications.DialogSpecifications[CharacterSelectActionsDialogSpecificationId]);
            
            View.GetComponent<Button>().onClick.AddListener(HandleClick);
        }

        protected override void AfterDispose()
        {
            View.GetComponent<Button>().onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            _dialogModel.SelectedUserId = _model.SelectedUserId;
                        
            if (!_dialogModel.IsOpened)
            {
                _gameModel.DialogsCollection.AddDialog(_dialogModel);
            }
        }
    }
}