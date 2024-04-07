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

        private Vector3 _smoothedPosition;
        private Quaternion _smoothedRotation;

        public CameraFollowUpdater(CameraModel cameraModel, CameraView cameraView)
        {
            _cameraModel = cameraModel;
            _cameraView = cameraView;
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
            var newTargetPosition = currentTarget.TransformPoint(cameraSpecification.Offset);
            newTargetPosition.y = cameraSpecification.Offset.y;
                    
            _smoothedPosition = Vector3.Lerp(_cameraView.Position, newTargetPosition, cameraSpecification.PositionSmoothTime);
            _smoothedPosition.y = cameraSpecification.Offset.y;
                    
            var targetRotation = currentTarget.localEulerAngles;
            targetRotation.x = cameraSpecification.InitialRotation.x;
                    
            _smoothedRotation = Quaternion.Slerp(_cameraView.Rotation, Quaternion.Euler(targetRotation), cameraSpecification.RotationSmoothTime);
            
            _cameraView.Rotate(_smoothedRotation);
            _cameraView.Follow(_smoothedPosition);
        }
    }
}