using Entities.Player;
using ServerCore.Main.Utilities;
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

            while (_timer >= ServerConst.TimeBetweenTicks)
            {
                _timer -= ServerConst.TimeBetweenTicks;
                HandleTick(deltaTime);
                _currentTick++;
            }
        }

        private void HandleTick(float deltaTime)
        {
            UpdatePhysics(deltaTime);
            HandleServerReconciliation();
        }

        private void UpdatePhysics(float deltaTime)
        {
            UnityEngine.Physics.simulationMode = SimulationMode.Script;
            _playerView.Rigidbody.isKinematic = false;
            _playerView.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _playerView.Rigidbody.angularVelocity = Vector3.zero;

            var serverData = _characterModel.ServerData;
            var serverPosition = serverData.LatestServerPosition.Value;
            var serverRotation = serverData.Rotation.Value;
            var offset = new Vector3(0, 0, 0);

            var newPosition = new Vector3(serverPosition.X / 100f, serverPosition.Y / 100f, serverPosition.Z / 100f) + offset;
            var newRotation = new Vector3(0, serverRotation / 100f, 0);
            var smoothedNewRotation = Vector3.Lerp(_playerView.LocalEulerAngles, newRotation, .5f);

            _characterModel.Position = Vector3.Lerp(_characterModel.Position, newPosition, .7f);
            _characterModel.NextRotation.Value = Quaternion.Euler(smoothedNewRotation);

            _playerView.Move(_characterModel.Position);
            _playerView.RotateEuler(smoothedNewRotation);

            var bufferIndex = _currentTick % 2048;
            var position = new Vector3(
                Mathf.FloorToInt(_characterModel.Position.x * 100f),
                Mathf.FloorToInt(_characterModel.Position.y * 100f),
                Mathf.FloorToInt(_characterModel.Position.z * 100f));

            _characterModel.MovementBuffer[bufferIndex] = new CharacterMovementState(_currentTick, position, Mathf.FloorToInt(_playerView.LocalEulerAngles.y * 100));
            
            UnityEngine.Physics.Simulate(deltaTime);
            UnityEngine.Physics.simulationMode = SimulationMode.FixedUpdate;
            _playerView.Rigidbody.isKinematic = true;
            _playerView.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        private void HandleServerReconciliation()
        {
            var serverStateBufferIndex = _characterModel.ServerData.CurrentTick.Value % 2048;
            var serverLastPosition = _characterModel.ServerData.LatestServerPosition.Value;
            var positionError = new Vector3(serverLastPosition.X, serverLastPosition.Y, serverLastPosition.Z) - _characterModel.MovementBuffer[serverStateBufferIndex].Position;
            
            if (positionError.sqrMagnitude > 0.001f)
            {
                var rewindTickCount = _characterModel.ServerData.CurrentTick.Value;

                while (rewindTickCount < _currentTick)
                {
                    var bufferIndex = rewindTickCount % 2048;
                    
                    _characterModel.MovementBuffer[bufferIndex].Position = new Vector3(
                        Mathf.FloorToInt(_characterModel.Position.x * 100f),
                        Mathf.FloorToInt(_characterModel.Position.y * 100f),
                        Mathf.FloorToInt(_characterModel.Position.z * 100f));;

                    _characterModel.MovementBuffer[bufferIndex].RotationY = Mathf.FloorToInt(_playerView.LocalEulerAngles.y * 100);

                    ++rewindTickCount;
                }
            }
        }
    }
}