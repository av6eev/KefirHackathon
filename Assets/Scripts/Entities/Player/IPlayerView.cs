using UnityEngine;

namespace Entities.Player
{
    public interface IPlayerView : IEntityView
    {
        void Move(Vector3 newPosition);
        void Rotate(Quaternion newRotation);
        void SetAnimationMovementSpeed(float normalizedSpeed);
        void EnableIdleAnimation();
        void DisableIdleAnimation();
    }
}