using Cameras.Specification;
using Input;
using UnityEngine;
using Updater;

namespace Cameras
{
    public class CameraFollowUpdater : IUpdater
    {
        private readonly CameraModel _cameraModel;
        private readonly CameraView _cameraView;
        private readonly IInputModel _inputModel;

        private Vector3 _smoothedPosition;
        private Vector3 _smoothedRotation;
        private Vector3 _velocity = Vector3.zero;
        
        public CameraFollowUpdater(CameraModel cameraModel, CameraView cameraView, IInputModel inputModel)
        {
            _cameraModel = cameraModel;
            _cameraView = cameraView;
            _inputModel = inputModel;
        }

        public void Update(float deltaTime)
        {
            var cameraSpecification = _cameraModel.Specification;
            var currentTarget = _cameraModel.CurrentTarget;
            
            if (cameraSpecification == null)
            {
                return;
            }
            
            switch (_cameraModel.CurrentState)
            {
                case CameraStateType.PlayerFollow:
                    HandlePlayerFollow(currentTarget, cameraSpecification);
                    break;
            }
        }

        private void HandlePlayerFollow(Transform currentTarget, CameraSpecification cameraSpecification)
        {
            var localEulerAngles = _cameraView.LocalEulerAngles;
            var newRotationX = localEulerAngles.x + -_inputModel.MouseDelta.y;
            // var newRotation = new Vector3(
                // Mathf.Clamp(newRotationX, cameraSpecification.InitialRotation.x - 3, cameraSpecification.InitialRotation.x + 3), 
                // localEulerAngles.y + _inputModel.MouseDelta.x, 
                // 0);
            
            var newRotation = new Vector3(
                cameraSpecification.InitialRotation.x, 
                localEulerAngles.y + (_inputModel.MouseDelta.x * cameraSpecification.HorizontalSensitivity), 
                0);
            
            _smoothedRotation = Vector3.SmoothDamp(_cameraView.LocalEulerAngles, newRotation, ref _velocity, cameraSpecification.RotationSmoothTime);
            _cameraView.Rotate(_smoothedRotation);

            var offset = new Vector3(
                _cameraView.Forward.x * cameraSpecification.Offset.x,
                _cameraView.Forward.y * cameraSpecification.Offset.y,
                _cameraView.Forward.z * Mathf.Abs(cameraSpecification.Offset.z)
            );
            var newTargetPosition = currentTarget.position - offset;
            
            _smoothedPosition = Vector3.Lerp(_cameraView.Position, newTargetPosition, cameraSpecification.PositionSmoothTime);
            _cameraView.Follow(_smoothedPosition);
        }
    }
}