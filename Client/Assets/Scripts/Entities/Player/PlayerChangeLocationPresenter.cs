using IPresenter = Presenter.IPresenter;

namespace Entities.Player
{
    public class PlayerChangeLocationPresenter : IPresenter
    {
        private readonly GameModel _gameModel;
        private readonly PlayerModel _model;

        public PlayerChangeLocationPresenter(GameModel gameModel, PlayerModel model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            _model.UserData.CurrentLocationId.Changed += HandleChangeLocation;
        }

        public void Dispose()
        {
            _model.UserData.CurrentLocationId.Changed -= HandleChangeLocation;
        }

        private void HandleChangeLocation()
        {
            if (!string.IsNullOrEmpty(_model.CurrentLocationId))
            {
                var oldSceneId = _gameModel.SceneManagementModelsCollection.GetModel(_model.CurrentLocationId).SceneId;
                _gameModel.SceneManagementModelsCollection.Unload(oldSceneId);    
            }
        }
    }
}