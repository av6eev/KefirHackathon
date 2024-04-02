using Cameras;
using Input;
using UnityEngine;
using Updater;

namespace Entities.Player.Physics
{
    public class PlayerPhysicsUpdater : IUpdater
    {
        private readonly IInputModel _inputModel;
        private readonly IGameModel _gameModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly ICameraModel _cameraModel;

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
            _playerView.Rigidbody.angularVelocity = Vector3.zero;
            
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
            else if (_playerModel.IsInputInverse)
            {
                MoveUpdate(-input, deltaTime);
                return;
            }
            
            MoveUpdate(input, deltaTime);
        }

        private void MoveUpdate(Vector3 input, float deltaTime)
        {
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
            
            if (input == Vector3.zero)
            {
                _playerModel.CurrentSpeed.Value = 0;
            }
            else
            {
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
        }
    }
}