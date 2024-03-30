using System;
using Specification;
using UnityEngine;

namespace Cameras.Specification
{
    [Serializable]
    public class CameraSpecification : BaseSpecification
    {
        public CameraStateType StateType;
        public Vector3 Offset;
        public Vector3 InitialRotation;
        public float HorizontalSensitivity;
        public float PositionSmoothTime;
        public float RotationSmoothTime;
        public float FreeLookPositionSmoothTime;
        public float FreeLookRotationSmoothTime;
        public float FieldOfView;
        public float NearClippingPlane;
        public float FarClippingPlane;
        public bool IsAwaitable;
    }
}