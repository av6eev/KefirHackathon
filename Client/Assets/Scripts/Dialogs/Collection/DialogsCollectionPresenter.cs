using Entities.Characters.Dialogs.Party;
using Entities.Characters.Dialogs.Party.Invite;
using Entities.Player.Dialog.Party;
using Presenter;

namespace Dialogs.Collection
{
    public class DialogsCollectionPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly DialogsCollection _model;
        private readonly DialogsCollectionView _view;

        private readonly PresentersDictionary<DialogModel> _presenters = new();
        
        public DialogsCollectionPresenter(IGameModel gameModel, DialogsCollection model, DialogsCollectionView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _model.AddEvent.OnChanged += AddDialog;
            _model.RemoveEvent.OnChanged += RemoveDialog;
        }

        public void Dispose()
        {
            _model.AddEvent.OnChanged -= AddDialog;
            _model.RemoveEvent.OnChanged -= RemoveDialog;
        }

        private void AddDialog(DialogModel model)
        {
            IPresenter presenter = null;
            
            switch (model)
            {
                case CharacterPartyInviteDialogModel dialogModel:
                    presenter = new CharacterPartyInviteDialogPresenter(_gameModel, dialogModel, _view);
                    break;
                case PlayerPartyDecisionDialogModel dialogModel:
                    presenter = new PlayerPartyDecisionDialogPresenter(_gameModel, dialogModel, _view);
                    break;
            }

            if (presenter != null)
            {
                presenter.Init();
            }
            
            _presenters.Add(model, presenter);
        }

        private void RemoveDialog(DialogModel model)
        {
            _presenters.Remove(model);
        }
    }
}