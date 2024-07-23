using Cameras;
using UnityEngine;
using Updater;

namespace Entities.Player
{
    public class PlayerCanvasViewUpdater : IUpdater
    {
        private readonly PlayerView _playerView;
        private readonly ICameraModel _cameraModel;

        public PlayerCanvasViewUpdater(PlayerView playerView, ICameraModel cameraModel)
        {
            _playerView = playerView;
            _cameraModel = cameraModel;
        }
        
        public void Update(float deltaTime)
        {
            var healthBarTransform = _playerView.HealthBarRoot.transform;
            var nicknameTransform = _playerView.NicknameText.gameObject.transform;
            
            healthBarTransform.rotation = Quaternion.LookRotation(healthBarTransform.position - _cameraModel.Position);
            nicknameTransform.rotation = Quaternion.LookRotation(nicknameTransform.position - _cameraModel.Position);
        }
    }
}