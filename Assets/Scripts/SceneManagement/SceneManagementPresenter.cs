using Loader.Scene;
using Presenter;

namespace SceneManagement
{
    public class SceneManagementPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SceneManagementModel _model;

        private ILoadSceneModel _currentScene;
        
        public SceneManagementPresenter(IGameModel gameModel, SceneManagementModel model)
        {
            _gameModel = gameModel;
            _model = model;
        }


        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}