using Presenter;

namespace Entities.Player
{
    public class PlayerChangeWorldPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly PlayerModel _model;

        public PlayerChangeWorldPresenter(GameModel gameModel, PlayerModel model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            _gameModel.WorldData.MessageType.Changed += HandleWorldChange;
        }

        public void Dispose()
        {
            _gameModel.WorldData.MessageType.Changed -= HandleWorldChange;
        }

        private void HandleWorldChange()
        {
            if (_gameModel.WorldData.MessageType.Value == "new")
            {
                var newLocationId = _model.UserData.CurrentLocationId.Value;
                _gameModel.SceneManagementModelsCollection.Load(newLocationId);

                _model.CurrentLocationId = newLocationId;
            }
        }
    }
}