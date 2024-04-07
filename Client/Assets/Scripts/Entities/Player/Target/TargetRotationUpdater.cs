using UnityEngine;
using Updater;

namespace Entities.Player.Target
{
    public class TargetRotationUpdater : IUpdater
    {
        private readonly PlayerModel _playerModel;

        public TargetRotationUpdater(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void Update(float deltaTime)
        {
            if (_playerModel.Target.Value == null) return;
            
            var dir = _playerModel.Target.Value.Position - _playerModel.Position;
            var angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            
            _playerModel.NextRotation.Value = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }
}