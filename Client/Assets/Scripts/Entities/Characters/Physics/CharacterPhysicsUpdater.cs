using Entities.Player;
using ServerCore.Main.Utilities;
using ServerManagement;
using ServerManagement.Test;
using UnityEngine;
using Updater;
using Vector3 = UnityEngine.Vector3;

namespace Entities.Characters.Physics
{
    public class CharacterPhysicsUpdater : IUpdater
    {
        private readonly CharacterModel _characterModel;
        private readonly PlayerView _playerView;

        private float _timer;
        private int _currentTick;

        public CharacterPhysicsUpdater(CharacterModel characterModel, PlayerView playerView)
        {
            _characterModel = characterModel;
            _playerView = playerView;
        }

        public void Update(float deltaTime)
        {
            _timer += deltaTime;

            if (_playerView != null)
            {
                UpdatePhysics(deltaTime);
            }
            
            if (_timer >= ClientConst.TimeBetweenTicks)
            {
                _timer = 0;
                _currentTick++;
            }
        }

        private void UpdatePhysics(float deltaTime)
        {
            var serverData = _characterModel.ServerData;
            var serverPosition = serverData.LatestServerPosition.Value;
            var offset = new Vector3(0, 0, 0);
            var convertedServerPosition = new Vector3(serverPosition.X / 100f, serverPosition.Y / 100f, serverPosition.Z / 100f) + offset;
            var direction = convertedServerPosition - _characterModel.Position;
            direction.y = 0;
            var newPosition = _characterModel.Position + direction * (_characterModel.CurrentSpeed.Value * deltaTime);

            if (direction != Vector3.zero)
            {
                var desiredRotation = Quaternion.LookRotation(direction, Vector3.up);
                var smoothedRotation = Quaternion.Slerp(_playerView.Rotation, desiredRotation, .2f);
                
                _characterModel.NextRotation.Value = smoothedRotation;
                _playerView.Rotate(smoothedRotation);
            }

            _characterModel.Position = Vector3.Lerp(_characterModel.Position, newPosition, .65f);
            _playerView.Move(_characterModel.Position);

            var bufferIndex = _currentTick % 2048;
            var position = new Vector3(
                Mathf.FloorToInt(_characterModel.Position.x * 100f),
                Mathf.FloorToInt(_characterModel.Position.y * 100f),
                Mathf.FloorToInt(_characterModel.Position.z * 100f));

            _characterModel.MovementBuffer[bufferIndex] = new CharacterMovementState(_currentTick, position, Mathf.FloorToInt(_playerView.LocalEulerAngles.y * 100));
        }
    }
}