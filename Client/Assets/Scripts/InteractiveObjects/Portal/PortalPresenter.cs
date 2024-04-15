using System.Linq;
using Presenter;

namespace InteractiveObjects.Portal
{
    public class PortalPresenter : IPresenter
    {
        private readonly IGameModel _gameModel;
        private readonly PortalView _view;
        
        private InteractiveObjectUpdater _portalUpdater;

        public PortalPresenter(IGameModel gameModel, PortalView view)
        {
            _gameModel = gameModel;
            _view = view;
        }
        
        public void Init()
        {
            _portalUpdater = new InteractiveObjectUpdater(_view.InteractiveObject, _gameModel.CameraModel);
            
            _gameModel.UpdatersList.Add(_portalUpdater);
            
            _gameModel.InputModel.OnInteracted += HandleInteract;
        }

        public void Dispose()
        {
            _gameModel.UpdatersList.Remove(_portalUpdater);

            _gameModel.InputModel.OnInteracted -= HandleInteract;
        }

        private void HandleInteract()
        {
            if (_view.InteractiveObject.IsInRange)
            {
                var fromSceneId = _gameModel.SceneManagementModelsCollection.GetModel(_view.FromSceneId).SceneId;
                
                _gameModel.SceneManagementModelsCollection.Unload(fromSceneId);
                _gameModel.SceneManagementModelsCollection.Load(_view.NextSceneId);
            }
        }
    }
}