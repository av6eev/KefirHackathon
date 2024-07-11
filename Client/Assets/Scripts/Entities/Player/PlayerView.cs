using System;
using Entities.Player.Dialog;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Entities.Player
{
    public class PlayerView : EntityView, IPlayerView, IPointerClickHandler
    {
        public PlayerDialogView DialogView;
        public event Action OnDamage;
        public event Action OnClick;
        
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

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("EnemySpell"))
            {
                OnDamage?.Invoke();
                Destroy(other.gameObject);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("CLICK FROM PLAYER VIEW");
            OnClick?.Invoke();
        }
    }
}