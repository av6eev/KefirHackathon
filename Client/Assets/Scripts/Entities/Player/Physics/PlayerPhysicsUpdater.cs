using Cameras;
using Input;
using ServerCore.Main.Commands;
using ServerCore.Main.Utilities;
using ServerManagement.Test;
using UnityEngine;
using Updater;
using Vector3 = UnityEngine.Vector3;

namespace Entities.Player.Physics
{
    public class PlayerPhysicsUpdater : IUpdater
    {
        private readonly IInputModel _inputModel;
        private readonly IGameModel _gameModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly ICameraModel _cameraModel;

        private float _timer;
        private int _currentTick;

        public PlayerPhysicsUpdater(IGameModel gameModel, PlayerModel playerModel, PlayerView playerView)
        {
            _gameModel = gameModel;
            _playerModel = playerModel;
            _playerView = playerView;

            _inputModel = gameModel.InputModel;
            _cameraModel = gameModel.CameraModel;
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
            var input = new Vector3(_inputModel.Direction.Value.x, 0, _inputModel.Direction.Value.y);

            if (_gameModel.SkillPanelModel.IsCasting)
            {
                return;
            }

            if (_playerModel.InDash.Value)
            {
                MoveUpdate(Vector3.forward, deltaTime);
                return;
            }

            if (_playerModel.IsInputInverse)
            {
                MoveUpdate(-input, deltaTime);
                return;
            }

            MoveUpdate(input, deltaTime);
        }

        private void MoveUpdate(Vector3 input, float deltaTime)
        {
            UnityEngine.Physics.simulationMode = SimulationMode.Script;
            _playerView.Rigidbody.isKinematic = false;
            _playerView.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _playerView.Rigidbody.angularVelocity = Vector3.zero;
            
            var newPosition = _playerView.Position;
            var specification = _playerModel.Specification;
            float constSpeed;

            if (_playerModel.InDash.Value)
            {
                constSpeed = specification.DashSpeed;
            }
            else if (_playerModel.IsRunning)
            {
                constSpeed = specification.RunSpeed;
            }
            else
            {
                constSpeed = specification.WalkSpeed;
            }

            if (input != Vector3.zero)
            {
                var bufferIndex = _currentTick % 2048;
                var position = new Vector3(
                    Mathf.FloorToInt(_playerModel.Position.x * 100f),
                    Mathf.FloorToInt(_playerModel.Position.y * 100f),
                    Mathf.FloorToInt(_playerModel.Position.z * 100f));

                _playerModel.MovementBuffer[bufferIndex] = new CharacterMovementState(_currentTick, position, Mathf.FloorToInt(_playerView.LocalEulerAngles.y * 100));
                
                _playerModel.CurrentSpeed.Value = Mathf.Lerp(_playerModel.CurrentSpeed.Value, constSpeed, .1f);
                var movementInput = Quaternion.Euler(0, _cameraModel.CurrentEulerAngles.y, 0) * input;
                var movementDirection = movementInput.normalized;

                if (!input.z.Equals(-1) && movementDirection != Vector3.zero && !_playerModel.InDash.Value)
                {
                    var desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    var smoothedRotation = Quaternion.Slerp(_playerView.Rotation, desiredRotation, specification.RotationSpeed * deltaTime);

                    _playerView.Rotate(smoothedRotation);
                }

                newPosition += movementDirection * (_playerModel.CurrentSpeed.Value * deltaTime);

                _playerView.Move(newPosition);
            }
            else
            {
                _playerModel.CurrentSpeed.Value = 0;
            }
            
            var command = new PlayerMovementCommand(
                _playerModel.Id,
                _playerModel.CurrentSpeed.Value,
                Mathf.FloorToInt(newPosition.x * 100),
                Mathf.FloorToInt(newPosition.y * 100),
                Mathf.FloorToInt(newPosition.z * 100),
                Mathf.FloorToInt(_playerView.LocalEulerAngles.y * 100),
                _currentTick);
                
            command.Write(_gameModel.ServerConnectionModel.PlayerPeer);
            
            UnityEngine.Physics.Simulate(ServerConst.TimeBetweenTicks);
            UnityEngine.Physics.simulationMode = SimulationMode.Update;
            _playerView.Rigidbody.isKinematic = true;
            _playerView.Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }
}