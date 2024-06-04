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
            _gameModel.UserData.CurrentLocationId.Changed += HandleChangeLocation;
        }

        public void Dispose()
        {
            _gameModel.UserData.CurrentLocationId.Changed -= HandleChangeLocation;
        }

        private void HandleChangeLocation()
        {
            var oldSceneId = _gameModel.SceneManagementModelsCollection.GetModel(_model.CurrentLocationId).SceneId;
            var newLocationId = _gameModel.UserData.CurrentLocationId.Value;
            
            _gameModel.SceneManagementModelsCollection.Unload(oldSceneId);
            _gameModel.SceneManagementModelsCollection.Load(newLocationId);

            _model.CurrentLocationId = newLocationId;
        }
    }
}