using Presenter;

namespace Entities.Player.Target
{
    public class PlayerTargetPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        
        private PlayerTargetUpdater _playerTargetUpdater;
        private TargetRotationUpdater _targetRotation;

        public PlayerTargetPresenter(IGameModel gameModel, PlayerModel model, PlayerView view)
        {
            _gameModel = gameModel;
            _model = model;
            _view = view;
        }

        public void Init()
        {
            _playerTargetUpdater = new PlayerTargetUpdater(_gameModel, _model, _view);
            _targetRotation = new TargetRotationUpdater(_model);

            _gameModel.UpdatersList.Add(_playerTargetUpdater);
            _gameModel.UpdatersList.Add(_targetRotation);
        }

        public void Dispose()
        {
            _gameModel.UpdatersList.Remove(_playerTargetUpdater);
            _gameModel.UpdatersList.Remove(_targetRotation);
        }
    }
}