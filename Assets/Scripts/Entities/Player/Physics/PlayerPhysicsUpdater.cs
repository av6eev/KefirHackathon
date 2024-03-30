using Cameras;
using Input;
using UnityEngine;
using Updater;

namespace Entities.Player.Physics
{
    public class PlayerPhysicsUpdater : IUpdater
    {
        private readonly IInputModel _inputModel;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly ICameraModel _cameraModel;

        public PlayerPhysicsUpdater(IInputModel inputModel, PlayerModel playerModel, PlayerView playerView, ICameraModel cameraModel)
        {
            _inputModel = inputModel;
            _playerModel = playerModel;
            _playerView = playerView;
            _cameraModel = cameraModel;
        }
        
        public void Update(float deltaTime)
        {
            var input = new Vector3(_inputModel.Direction.Value.x, 0, _inputModel.Direction.Value.y);

            if (input == Vector3.zero)
            {
                _playerModel.CurrentSpeed.Value = 0f;
                return;
            }

            MoveUpdate(input, deltaTime);
        }

        private void MoveUpdate(Vector3 input, float deltaTime)
        {
            var newPosition = _playerView.Position;
            var specification = _playerModel.Specification;
            var constSpeed = _playerModel.IsRunning ? specification.RunSpeed : specification.WalkSpeed;
            
            _playerModel.CurrentSpeed.Value = Mathf.Lerp(_playerModel.CurrentSpeed.Value, constSpeed, .1f);

            var movementInput = Quaternion.Euler(0, _cameraModel.CurrentEulerAngles.y, 0) * input;
            var movementDirection = movementInput.normalized;

            if (!input.z.Equals(-1) && movementDirection != Vector3.zero)
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