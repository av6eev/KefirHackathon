using UnityEngine;
using UnityEngine.AI;

namespace Entities.Enemy
{
    public class EnemyView : EntityView
    {
        public NavMeshAgent NavMeshAgent;
        public GameObject InTargetCircle;
        
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
    }
}