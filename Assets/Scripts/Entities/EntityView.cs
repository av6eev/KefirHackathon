using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public abstract class EntityView : MonoBehaviour, IEntityView
    {
        protected readonly int IsMovement = Animator.StringToHash("IsMovement");
        protected readonly int Speed = Animator.StringToHash("Speed");
        
        public Rigidbody Rigidbody;
        public Transform Root;
        public EntityType Type;
        public Image HealthBar;
        public GameObject WeaponSlot;
        public Animator EntityAnimatorController;
        public LayerMask EnemyLayer;

        public Vector3 Forward => Root.forward;
        public Vector3 Right => Root.right;
        public Vector3 Position => Root.position;
        public Quaternion Rotation => Root.rotation;
        public Vector3 LocalEulerAngles => Root.localEulerAngles;

        public virtual void SetForward(Vector3 heading)
        {
            Root.forward = heading;
        }

        public Vector3 TransformPoint(Vector3 offset)
        {
            return Root.TransformPoint(offset);
        }

        public virtual void Move(Vector3 newPosition)
        {
            Root.position = newPosition;
        }

        public virtual void Rotate(Quaternion newRotation)
        {
            Root.rotation = newRotation;
        }
    }

    public interface IEntityView
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Vector3 Right { get; }
        Quaternion Rotation { get; }
        Vector3 LocalEulerAngles { get; }
        void SetForward(Vector3 heading);
        Vector3 TransformPoint(Vector3 cameraSpecificationOffset);
    }
}