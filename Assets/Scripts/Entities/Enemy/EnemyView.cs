using System;
using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy
{
    public class EnemyView : EntityView
    {
        public NavMeshAgent NavMeshAgent;
        public GameObject InTargetCircle;
        public Transform RotateBoneGo;
        private bool _move;
        private bool _hit;
        private float _shiftTime = .1f;
        private float _currentShiftTime;

        private static readonly int IsMovement = UnityEngine.Animator.StringToHash("IsMovement");
        private static readonly int Speed = UnityEngine.Animator.StringToHash("Speed");

        public void SetAnimationMovementSpeed(float normalizedSpeed)
        {
            EntityAnimatorController.SetFloat(Speed, normalizedSpeed);
        }
        
        public void EnableIdleAnimation()
        {
            EntityAnimatorController.SetBool(IsMovement, false);
        }
        
        public void DisableIdleAnimation()
        {
            EntityAnimatorController.SetBool(IsMovement, true);
        }

        public void RotateBone()
        {
            if (!_hit)
            {
                _hit = true;
            }
        }
        
        private void Update()
        {
            if (_hit)
            {
                _currentShiftTime = _shiftTime;
            }

            _move = _currentShiftTime > 0;
            _currentShiftTime -= Time.deltaTime;
            _currentShiftTime = Mathf.Clamp01(_currentShiftTime);
        }

        private void LateUpdate()
        {
            if (_move)
            {
                RotateBoneGo.Rotate(new Vector3(25,25,25));
                _move = false;
            }
        }
    }
}