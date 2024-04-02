using Loader.Scene;
using Presenter;

namespace SceneManagement.Collection
{
    public class SceneManagementModelsCollectionPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly SceneManagementModelsCollection _model;

        private ILoadSceneModel _currentScene;
        
        public SceneManagementModelsCollectionPresenter(IGameModel gameModel, SceneManagementModelsCollection model)
        {
            _gameModel = gameModel;
            _model = model;
        }
        
        public void Init()
        {
            _model.AddEvent.OnChanged += HandleAdd;
            _model.RemoveEvent.OnChanged += HandleRemove;
        }

        public void Dispose()
        {
            _model.AddEvent.OnChanged -= HandleAdd;
            _model.RemoveEvent.OnChanged -= HandleRemove;
        }

        private void HandleAdd(SceneManagementModel model)
        {
            _currentScene = _gameModel.LoadScenesModel.Load(_gameModel.Specifications.SceneSpecifications[model.SceneId]);
        }

        private void HandleRemove(SceneManagementModel model)
        {
            _gameModel.LoadScenesModel.Unload(_currentScene);
        }
    }
}