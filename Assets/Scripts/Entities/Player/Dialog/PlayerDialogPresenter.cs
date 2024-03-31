using Presenter;

namespace Entities.Player.Dialog
{
    public class PlayerDialogPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly PlayerDialogModel _model;
        private readonly PlayerDialogView _view;
        
        private PlayerDialogUpdater _updater;

        public PlayerDialogPresenter(IGameModel gameModel, PlayerDialogModel model, PlayerDialogView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }
        
        public void Init()
        {
            _updater = new PlayerDialogUpdater(_model, _view);
            _gameModel.UpdatersList.Add(_updater);
        }

        public void Dispose()
        {
            _gameModel.UpdatersList.Remove(_updater);
        }
    }
}