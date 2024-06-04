using System.Linq;
using Presenter;
using ServerCore.Main.Commands;
using UnityEngine;

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
                var command = new ChangeLocationCommand(_gameModel.PlayerModel.Id, _view.NextSceneId);
                command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            }
        }
    }
}