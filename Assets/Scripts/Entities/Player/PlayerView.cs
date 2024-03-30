﻿namespace Entities.Player
{
    public class PlayerView : EntityView, IPlayerView
    {
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