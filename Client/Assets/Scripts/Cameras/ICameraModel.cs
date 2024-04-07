using Cameras.Specification;
using UnityEngine;

namespace Cameras
{
    public interface ICameraModel
    {
        CameraStateType CurrentState { get; }
        CameraSpecification Specification { get; }
        Vector3 CurrentEulerAngles { get; }
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Vector3 Right { get; }
        void ChangeState(CameraStateType newStateType, Transform newTarget = null);
    }
}