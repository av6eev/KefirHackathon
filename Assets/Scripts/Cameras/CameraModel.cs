using System;
using Awaiter;
using Cameras.Specification;
using Reactive.Field;
using UnityEngine;

namespace Cameras
{
    public class CameraModel : ICameraModel
    {
        public event Action<CameraStateType> OnStateChanged; 
        
        public CameraSpecification Specification { get; set; }

        public Transform CurrentTarget { get; set; }
        public Transform NextTarget { get; set; }

        public ReactiveField<bool> IsCompleted { get; } = new();
        public CameraStateType NextState { get; set; }
        public CameraStateType CurrentState { get; set; }

        public CustomAwaiter CurrentStateCompleteAwaiter { get; set; } = new();
        
        public Vector3 CurrentEulerAngles { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Forward { get; set; }
        public Vector3 Right { get; set; }

        public void ChangeState(CameraStateType newStateType, Transform newTarget = null)
        {
            if (CurrentState == newStateType)
            {
                Debug.Log($"{newStateType} is already current state!");
                return;
            }
            
            if (NextState == newStateType)
            {
                Debug.Log($"{newStateType} is already next state!");
                return;
            }

            NextTarget = newTarget;
            NextState = newStateType;

            Debug.Log("New next state: " + NextState);
            Debug.Log("Current state: " + CurrentState);
            
            OnStateChanged?.Invoke(newStateType);
        }
    }
}