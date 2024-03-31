using System;
using Entities.Player.Dialog;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerView : EntityView, IPlayerView
    {
        public PlayerDialogView DialogView;
        public event Action OnDamage; 
        
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemySpell"))
            {
                OnDamage?.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}